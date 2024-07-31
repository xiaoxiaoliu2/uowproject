using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using uowpublic.Models;
using uowpublic.Data;

namespace uowpublic.Services
{
    public interface IPostService
    {
        Task CreateAsync(Post newPost);
        Task<List<Post>> GetAsync();
        Task<Post?> GetAsync(int id);
        Task RemoveAsync(int id);
        Task UpdateAsync(int id, Post updatedPost);

        Task<List<Post_Photo>> GetPhotosForPostAsync(int postId);
        Task<List<Post_Tag_Output>> GetTagsForPostAsync(int postId);
        Task<List<Post>> GetPostsForTagAsync(int tagId);
    }

    public class PostService : IPostService
    {
        private readonly DatabaseContext _context;

        public PostService(DatabaseContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Post newPost)
        {
            var (tags, photos) = (newPost.Tags, newPost.Photos);
            newPost.Tags = null;
            newPost.Photos = null;

            _context.Post.Add(newPost);
            await _context.SaveChangesAsync();
            
            // add Tag
            foreach (var tag in tags ?? Enumerable.Empty<Post_Tag_Output>())
            {
                var newTag = new Post_Tag
                {
                    Post_Id = tag.Post_Id,
                    Tag_Id = tag.Tag_Id,
                    IsDeleted = tag.IsDeleted
                };
                _context.Set<Post_Tag>().Add(newTag);
            }
            await _context.SaveChangesAsync();

            // add PostPhoto
            foreach (var photo in photos ?? Enumerable.Empty<Post_Photo>())
            {
                var newPhoto = new Post_Photo
                {
                    Post_Id = photo.Post_Id,
                    Url = photo.Url,
                    IsDeleted = photo.IsDeleted
                };
                _context.Set<Post_Photo>().Add(newPhoto);
            }
            // save
            await _context.SaveChangesAsync();
        }

        public async Task<List<Post>> GetAsync()
        {
            List<Post> posts = await _context.Post.Where(p => p.IsDeleted == false).ToListAsync();
            foreach (var post in posts)
            {
                // get post tags
                List<Post_Tag_Output> tags = await GetTagsForPostAsync(post.Id);
                post.Tags = tags;

                // get post photos
                List<Post_Photo> photos = await GetPhotosForPostAsync(post.Id);
                post.Photos = photos;
            }
            return posts;
        }

        public async Task<Post?> GetAsync(int id)
        {
            return await _context.Post.FindAsync(id);
        }

        public async Task RemoveAsync(int id)
        {
            var post = await _context.Post.Where(p => p.Id == id && !p.IsDeleted).FirstOrDefaultAsync();
            if (post != null)
            {
                post.IsDeleted = true;
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateAsync(int id, Post updatedPost)
        {
            var post = await _context.Post.FindAsync(id);
            if (post != null)
            {
                post.User_Id = updatedPost.User_Id;
                post.Title = updatedPost.Title;
                post.Content = updatedPost.Content;
                post.Created_At = updatedPost.Created_At;
                post.IsDeleted = updatedPost.IsDeleted;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Post_Photo>> GetPhotosForPostAsync(int postId)
        {
            return await _context.Post_Photo
                .Where(pp => pp.Post_Id == postId && !pp.IsDeleted)
                .Select(pp => new Post_Photo
                {
                    Id = pp.Id,
                    Post_Id = pp.Post_Id,
                    Url = pp.Url,
                    IsDeleted = pp.IsDeleted
                })
                .ToListAsync();
        }

        public async Task<List<Post_Tag_Output>> GetTagsForPostAsync(int postId)
        {
            return await _context.Post_Tag
                .Where(pt => pt.Post_Id == postId && !pt.IsDeleted)
                .Join(_context.Tag, pt => pt.Tag_Id, t => t.Id, (pt, t) => new
                {
                    pt,
                    t
                })
                .Select(joinResult => new Post_Tag_Output
                {
                    Post_Id = joinResult.pt.Post_Id,
                    Tag_Id = joinResult.pt.Tag_Id,
                    Name = joinResult.t.Name,
                    IsDeleted = joinResult.pt.IsDeleted
                })
                .ToListAsync();
        }

        public async Task<List<Post>> GetPostsForTagAsync(int tagId)
        {
            return await _context.Post_Tag
                .Where(pt => pt.Tag_Id == tagId && !pt.IsDeleted)
                .Join(_context.Post, pt => pt.Post_Id, p => p.Id, (pt, p) => p)
                .ToListAsync();
        }
    }
}

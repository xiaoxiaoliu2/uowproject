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

        Task<List<PostPhoto>> GetPhotosForPostAsync(int postId);
        Task<List<Tag>> GetTagsForPostAsync(int postId);
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
            _context.Post.Add(newPost);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Post>> GetAsync()
        {
            return await _context.Post.Where(p => p.IsDeleted == false).ToListAsync();
        }

        public async Task<Post?> GetAsync(int id)
        {
            return await _context.Post.FindAsync(id);
        }

        public async Task RemoveAsync(int id)
        {
            var post = await _context.Post.FindAsync(id);
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

        public async Task<List<PostPhoto>> GetPhotosForPostAsync(int postId)
        {
            return await _context.PostPhoto.Where(pp => pp.PostId == postId && pp.IsDeleted == false).ToListAsync();
        }

        public async Task<List<Tag>> GetTagsForPostAsync(int postId)
        {
            return await _context.PostTag
                .Where(pt => pt.PostId == postId && pt.IsDeleted == false)
                .Join(_context.Tag, pt => pt.TagId, t => t.Id, (pt, t) => t)
                .ToListAsync();
        }

        public async Task<List<Post>> GetPostsForTagAsync(int tagId)
        {
            return await _context.PostTag
                .Where(pt => pt.TagId == tagId && pt.IsDeleted == false)
                .Join(_context.Post, pt => pt.PostId, p => p.Id, (pt, p) => p)
                .ToListAsync();
        }
    }
}

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
            return await _context.Post.ToListAsync();
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
                _context.Post.Remove(post);
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
    }
}

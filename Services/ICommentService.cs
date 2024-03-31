using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using uowpublic.Models;
using uowpublic.Data;

namespace uowpublic.Services
{
    public interface ICommentService
    {
        Task CreateAsync(Comment newComment);
        Task<List<Comment>> GetAsync();
        Task<Comment?> GetAsync(int id);
        Task RemoveAsync(int id);
        Task UpdateAsync(int id, Comment updatedComment);
    }

    public class CommentService : ICommentService
    {
        private readonly DatabaseContext _context;

        public CommentService(DatabaseContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Comment newComment)
        {
            _context.Comment.Add(newComment);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Comment>> GetAsync()
        {
            return await _context.Comment.ToListAsync();
        }

        public async Task<Comment?> GetAsync(int id)
        {
            return await _context.Comment.FindAsync(id);
        }

        public async Task RemoveAsync(int id)
        {
            var comment = await _context.Comment.FindAsync(id);
            if (comment != null)
            {
                _context.Comment.Remove(comment);
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateAsync(int id, Comment updatedComment)
        {
            var comment = await _context.Comment.FindAsync(id);
            if (comment != null)
            {
                comment.User_Id = updatedComment.User_Id;
                comment.Post_Id = updatedComment.Post_Id;
                comment.Content = updatedComment.Content;
                comment.Created_At = updatedComment.Created_At;
                comment.IsDeleted = updatedComment.IsDeleted;

                await _context.SaveChangesAsync();
            }
        }
    }
}

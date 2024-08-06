using Microsoft.EntityFrameworkCore;
using StockAPI.Data;
using StockAPI.Interfaces;
using StockAPI.Models;

namespace StockAPI.Repository
{
    public class CommantRepository : ICommentRepository
    {
        private readonly ApplicationDBContext _context;

        public CommantRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<List<Comment>> GetCommentsAsync()
        {
            return await _context.Comments.Include(a => a.AppUser).ToListAsync();
        }

        public async Task<Comment?> GetCommentAsync(int id)
        {
            var comment = await _context
                .Comments.Include(a => a.AppUser)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (comment == null)
            {
                return null;
            }

            return comment;
        }

        public async Task<Comment> CreateCommentAsync(Comment comment)
        {
            await _context.Comments.AddAsync(comment);
            await _context.SaveChangesAsync();
            return comment;
        }

        public async Task<Comment?> UpdateCommentAsync(int id, Comment updateDto)
        {
            var existingComment = await _context.Comments.FindAsync(id);

            if (existingComment == null)
            {
                return null;
            }

            existingComment.Title = updateDto.Title;
            existingComment.Content = updateDto.Content;

            await _context.SaveChangesAsync();

            return existingComment;
        }

        public async Task<Comment?> DeleteCommentAsync(int id)
        {
            var comment = await _context.Comments.FirstOrDefaultAsync(c => c.Id == id);

            if (comment == null)
            {
                return null;
            }

            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();

            return comment;
        }
    }
}

using StockAPI.Helpers;
using StockAPI.Models;

namespace StockAPI.Interfaces
{
    public interface ICommentRepository
    {
        Task<List<Comment>> GetCommentsAsync(CommentQueryObject queryObj);

        Task<Comment?> GetCommentAsync(int id);

        Task<Comment> CreateCommentAsync(Comment comment);

        Task<Comment?> UpdateCommentAsync(int id, Comment updateDto);

        Task<Comment?> DeleteCommentAsync(int id);
    }
}

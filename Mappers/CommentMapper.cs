using CommentAPI.Controllers;
using StockAPI.Dtos.Comment;
using StockAPI.Models;

namespace StockAPI.Mappers
{
    public static class CommentMapper
    {
        public static CommentDto ToCommentDto(this Comment comment)
        {
            return new CommentDto
            {
                Id = comment.Id,
                StockId = comment.StockId,
                Title = comment.Title,
                Content = comment.Content,
                CreatedAt = comment.CreatedAt,
                CreatedBy = comment.AppUser?.UserName,
            };
        }

        public static Comment ToCommentFromCreateDto(
            this CreateCommentRequestDto commentDto,
            int stockId
        )
        {
            return new Comment
            {
                StockId = stockId,
                Title = commentDto.Title,
                Content = commentDto.Content,
            };
        }

        public static Comment ToCommentFromUpdateDto(this UpdateCommentRequestDto commentDto)
        {
            return new Comment { Title = commentDto.Title, Content = commentDto.Content, };
        }
    }
}

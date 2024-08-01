using System.ComponentModel.DataAnnotations;

namespace StockAPI.Mappers
{
    public class CreateCommentRequestDto
    {
        [Required]
        [MinLength(5, ErrorMessage = "Author must be at least 5 characters long")]
        [MaxLength(50, ErrorMessage = "Author must be at most 50 characters long")]
        public string Author { get; set; } = string.Empty;

        [Required]
        [MinLength(5, ErrorMessage = "Title must be at least 5 characters long")]
        [MaxLength(100, ErrorMessage = "Title must be at most 100 characters long")]
        public string Title { get; set; } = string.Empty;

        [Required]
        [MinLength(5, ErrorMessage = "Content must be at least 5 characters long")]
        [MaxLength(280, ErrorMessage = "Content must be at most 280 characters long")]
        public string Content { get; set; } = string.Empty;
    }
}

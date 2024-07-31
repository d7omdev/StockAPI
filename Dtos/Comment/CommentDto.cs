namespace StockAPI.Dtos.Comment
{
    public class CommentDto
    {
        public int Id { get; set; }

        public int? StockId { get; set; }

        public string Author { get; set; } = string.Empty;

        public string Title { get; set; } = string.Empty;

        public string Content { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}

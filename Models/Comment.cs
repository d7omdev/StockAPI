namespace StockAPI.Models
{
    public class Comment
    {
        public int Id { get; set; }

        public int? StockId { get; set; } // Navigation property

        public Stock? Stock { get; set; }

        public string Author { get; set; } = string.Empty;

        public string Title { get; set; } = string.Empty;

        public string Content { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}

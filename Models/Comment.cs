using System.ComponentModel.DataAnnotations.Schema;

namespace StockAPI.Models
{
    [Table("Comments")]
    public class Comment
    {
        public int Id { get; set; }

        public int? StockId { get; set; } // Navigation property

        public Stock? Stock { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Content { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public string AppUserId { get; set; } // Navigation property

        public AppUser AppUser { get; set; }
    }
}

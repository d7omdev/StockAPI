namespace StockAPI.Mappers
{
    public class CreateCommentRequestDto
    {
        public string Author { get; set; } = string.Empty;

        public string Title { get; set; } = string.Empty;

        public string Content { get; set; } = string.Empty;
    }
}

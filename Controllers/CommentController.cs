using Microsoft.AspNetCore.Mvc;
using StockAPI.Interfaces;
using StockAPI.Mappers;

namespace CommentAPI.Controllers
{
    [Route("api/comment")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository _commentRepo;

        private readonly IStockRepository _stockRepo;

        public CommentController(ICommentRepository commentRepo, IStockRepository stockRepo)
        {
            _commentRepo = commentRepo;
            _stockRepo = stockRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetComments()
        {
            var comments = await _commentRepo.GetCommentsAsync();

            var commentsDto = comments.Select(r => r.ToCommentDto());

            if (comments == null)
            {
                return NotFound();
            }

            return Ok(commentsDto);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetComment([FromRoute] int id)
        {
            var comment = await _commentRepo.GetCommentAsync(id);
            if (comment == null)
            {
                return NotFound();
            }
            return Ok(comment.ToCommentDto());
        }

        [HttpPost("{stockId:int}")]
        public async Task<IActionResult> CreateComment(
            [FromRoute] int stockId,
            [FromBody] CreateCommentRequestDto commentDto
        )
        {
            if (!await _stockRepo.StockExists(stockId))
            {
                return BadRequest("Stock does not exist!");
            }

            var comment = commentDto.ToCommentFromCreateDto(stockId);
            await _commentRepo.CreateCommentAsync(comment);

            return CreatedAtAction(
                nameof(GetComment),
                new { id = comment.Id },
                comment.ToCommentDto()
            );
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateComment(
            [FromRoute] int id,
            [FromBody] UpdateCommentRequestDto updateDto
        )
        {
            var comment = await _commentRepo.UpdateCommentAsync(
                id,
                updateDto.ToCommentFromUpdateDto()
            );
            if (comment == null)
            {
                return NotFound("Comment not found!");
            }

            return Ok(comment.ToCommentDto());
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteComment([FromRoute] int id)
        {
            var comment = await _commentRepo.DeleteCommentAsync(id);

            if (comment == null)
            {
                return NotFound();
            }

            return Ok("Deleted comment");
        }
    }
}

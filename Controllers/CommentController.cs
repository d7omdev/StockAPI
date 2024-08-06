using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StockAPI.Extentions;
using StockAPI.Interfaces;
using StockAPI.Mappers;
using StockAPI.Models;

namespace CommentAPI.Controllers
{
    [Route("api/comment")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository _commentRepo;

        private readonly IStockRepository _stockRepo;

        private readonly UserManager<AppUser> _userManager;

        public CommentController(
            ICommentRepository commentRepo,
            IStockRepository stockRepo,
            UserManager<AppUser> userManager
        )
        {
            _commentRepo = commentRepo;
            _stockRepo = stockRepo;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetComments()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

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
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
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
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (!await _stockRepo.StockExists(stockId))
            {
                return BadRequest("Stock does not exist!");
            }

            var username = User.GetUsername();
            var appUser = await _userManager.FindByNameAsync(username);
            if (appUser == null)
            {
                return Unauthorized();
            }

            var comment = commentDto.ToCommentFromCreateDto(stockId);
            comment.AppUserId = appUser.Id;
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
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
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
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var comment = await _commentRepo.DeleteCommentAsync(id);

            if (comment == null)
            {
                return NotFound();
            }

            return Ok("Deleted comment");
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using StockAPI.Data;
using StockAPI.Dtos.Stock;
using StockAPI.Interfaces;
using StockAPI.Mappers;

namespace StockAPI.Controllers
{
    [Route("api/stock")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly ApplicationDBContext _context;

        private readonly IStockRepository _stockRepo;

        public StockController(ApplicationDBContext context, IStockRepository stockRepo)
        {
            _stockRepo = stockRepo;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetStocks()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var stocks = await _stockRepo.GetStocksAsync();

            var stocksDto = stocks.Select(s => s.ToStockDto());

            if (stocks == null)
            {
                return NotFound();
            }

            return Ok(stocks);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetStock([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var stock = await _stockRepo.GetStockAsync(id);
            if (stock == null)
            {
                return NotFound();
            }
            return Ok(stock.ToStockDto());
        }

        [HttpPost]
        public async Task<IActionResult> CreateStock([FromBody] CreateStockRequestDto stockDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var stock = stockDto.ToStockFromDto();
            await _stockRepo.CreateStockAsync(stock);

            return CreatedAtAction(nameof(GetStock), new { id = stock.Id }, stock.ToStockDto());
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateStock(
            [FromRoute] int id,
            [FromBody] UpdateStockRequestDto updateDto
        )
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var stock = await _stockRepo.UpdateStockAsync(id, updateDto);
            if (stock == null)
            {
                return NotFound();
            }

            return Ok(stock.ToStockDto());
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteStock([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var stock = await _stockRepo.DeleteStockAsync(id);
            if (stock == null)
            {
                return NotFound();
            }

            return Ok("Stock deleted");
        }
    }
}

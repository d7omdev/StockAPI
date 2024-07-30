using Microsoft.AspNetCore.Mvc;
using StockAPI.Data;
using StockAPI.Dtos.Stock;
using StockAPI.Mappers;

namespace StockAPI.Controllers
{
    [Route("api/stock")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly ApplicationDBContext _context;

        public StockController(ApplicationDBContext Context)
        {
            _context = Context;
        }

        [HttpGet]
        public IActionResult GetStocks()
        {
            var stocks = _context.Stocks.ToList().Select(stock => stock.ToStockDto());
            if (stocks == null)
            {
                return NotFound();
            }
            return Ok(stocks);
        }

        [HttpGet("{id}")]
        public IActionResult GetStock([FromRoute] int id)
        {
            var stock = _context.Stocks.Find(id);
            if (stock == null)
            {
                return NotFound();
            }
            return Ok(stock.ToStockDto());
        }

        [HttpPost]
        public IActionResult CreateStock([FromBody] CreateStockRequestDto stockDto)
        {
            var stock = stockDto.ToStockFromDto();
            _context.Stocks.Add(stock);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetStock), new { id = stock.Id }, stock.ToStockDto());
        }
    }
}

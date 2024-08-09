using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StockAPI.Extentions;
using StockAPI.Interfaces;
using StockAPI.Models;

namespace StockAPI.Controllers
{
    [Route("api/portfolio")]
    [ApiController]
    public class PortfolioController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IStockRepository _stockRepo;
        private readonly IPortfolioRepository _portfolioRepo;
        private readonly IFMPService _fmpService;

        public PortfolioController(
            UserManager<AppUser> userManager,
            IStockRepository stockRepo,
            IPortfolioRepository portfolioRepo,
            IFMPService fmpService
        )
        {
            _userManager = userManager;
            _stockRepo = stockRepo;
            _portfolioRepo = portfolioRepo;
            _fmpService = fmpService;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetUserPortfolio()
        {
            var username = User.GetUsername();
            Console.WriteLine("UserName:", username);
            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
                return Unauthorized();
            var userPortfolio = await _portfolioRepo.GetUserPortfolio(user);
            return Ok(userPortfolio);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreatePortfolio(string symbol)
        {
            var username = User.GetUsername();
            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
                return Unauthorized();

            var stock = await _stockRepo.GetStockBySymbolAsync(symbol);

            if (stock == null)
            {
                stock = await _fmpService.FindStockBySymbolAsync(symbol);
                if (stock == null)
                {
                    return BadRequest("This Stock does not exist!");
                }
                else
                {
                    await _stockRepo.CreateStockAsync(stock);
                }
            }

            if (stock == null)
                return BadRequest("Stock does not exist!");

            var userPortfolio = await _portfolioRepo.GetUserPortfolio(user);
            if (userPortfolio.Any(s => s.Symbol == symbol))
                return BadRequest("Stock already exists in portfolio");

            var portfolio = new Portfolio { StockId = stock.Id, AppUserId = user.Id };
            await _portfolioRepo.CreatePortfolioAsync(portfolio);
            return portfolio == null ? BadRequest("Failed to add stock to portfolio") : Created();
        }

        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> DeletePortfolio(string symbol)
        {
            var username = User.GetUsername();
            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
                return Unauthorized();

            var userPortfolio = await _portfolioRepo.GetUserPortfolio(user);
            var filterdStock = userPortfolio.Where(s => s.Symbol == symbol).ToList();
            if (filterdStock.Count() == 1)
            {
                await _portfolioRepo.DeletePortfolioAsync(user, symbol);
            }
            else
            {
                return BadRequest("Stock does not exist in portfolio");
            }
            return Ok();
        }
    }
}

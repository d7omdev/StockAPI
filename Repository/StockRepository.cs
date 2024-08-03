using Microsoft.EntityFrameworkCore;
using StockAPI.Data;
using StockAPI.Dtos.Stock;
using StockAPI.Helpers;
using StockAPI.Interfaces;
using StockAPI.Models;

namespace StockAPI.Repository
{
    class StockRepository : IStockRepository
    {
        private readonly ApplicationDBContext _context;

        public StockRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<List<Stock>> GetStocksAsync(QueryObject query)
        {
            var stocks = _context.Stocks.Include(c => c.Comments).AsQueryable();

            if (!string.IsNullOrWhiteSpace(query.Symbol))
            {
                stocks = stocks.Where(s => s.Symbol.Contains(query.Symbol));
            }

            if (!string.IsNullOrWhiteSpace(query.CompanyName))
            {
                stocks = stocks.Where(s => s.CompanyName.Contains(query.CompanyName));
            }

            if (!string.IsNullOrWhiteSpace(query.SortBy))
            {
                if (query.SortBy.Equals("Symbol", StringComparison.OrdinalIgnoreCase))
                {
                    stocks = query.IsDecsending
                        ? stocks.OrderByDescending(s => s.Symbol)
                        : stocks.OrderBy(s => s.Symbol);
                }
            }

            if (!string.IsNullOrWhiteSpace(query.SortBy))
            {
                if (query.SortBy.Equals("CompanyName", StringComparison.OrdinalIgnoreCase))
                {
                    stocks = query.IsDecsending
                        ? stocks.OrderByDescending(s => s.CompanyName)
                        : stocks.OrderBy(s => s.CompanyName);
                }
            }

            var skipNumber = (query.PageNumber - 1) * query.PageSize;

            stocks = stocks.Skip(skipNumber).Take(query.PageSize);

            return await stocks.ToListAsync();
        }

        public Task<Stock?> GetStockAsync(int id)
        {
            var stock = _context.Stocks.Include(c => c.Comments).FirstOrDefault(s => s.Id == id);

            return Task.FromResult(stock);
        }

        public async Task<Stock> CreateStockAsync(Stock stock)
        {
            await _context.Stocks.AddAsync(stock);
            await _context.SaveChangesAsync();
            return stock;
        }

        public async Task<Stock?> UpdateStockAsync(int id, UpdateStockRequestDto stockDto)
        {
            var stockToUpdate = await _context.Stocks.FirstOrDefaultAsync(x => x.Id == id);

            if (stockToUpdate == null)
            {
                return null;
            }

            stockToUpdate.Symbol = stockDto.Symbol;
            stockToUpdate.Purchase = stockDto.Purchase;
            stockToUpdate.CompanyName = stockDto.CompanyName;
            stockToUpdate.LastDiv = stockDto.LastDiv;
            stockToUpdate.Industry = stockDto.Industry;
            stockToUpdate.MarketCap = stockDto.MarketCap;

            await _context.SaveChangesAsync();
            return stockToUpdate;
        }

        public async Task<Stock?> DeleteStockAsync(int id)
        {
            var stock = await _context.Stocks.FirstOrDefaultAsync(x => x.Id == id);

            if (stock == null)
            {
                return null;
            }

            _context.Stocks.Remove(stock);
            await _context.SaveChangesAsync();
            return stock;
        }

        public Task<bool> StockExists(int id)
        {
            return _context.Stocks.AnyAsync(s => s.Id == id);
        }
    }
}

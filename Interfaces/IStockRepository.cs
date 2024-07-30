using StockAPI.Dtos.Stock;
using StockAPI.Models;

namespace StockAPI.Interfaces
{
    public interface IStockRepository
    {
        Task<List<Stock>> GetStocksAsync();
        Task<Stock?> GetStockAsync(int id);
        Task<Stock> CreateStockAsync(Stock stock);
        Task<Stock?> UpdateStockAsync(int id, UpdateStockRequestDto stock);
        Task<Stock?> DeleteStockAsync(int id);
    }
}

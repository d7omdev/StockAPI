using api.Dtos.Stock;
using StockAPI.Dtos.Stock;
using StockAPI.Models;

namespace StockAPI.Mappers
{
    public static class StockMappers
    {
        public static StockDto ToStockDto(this Stock stockModel)
        {
            return new StockDto
            {
                Id = stockModel.Id,
                Symbol = stockModel.Symbol,
                CompanyName = stockModel.CompanyName,
                Purchase = stockModel.Purchase,
                LastDiv = stockModel.LastDiv,
                Industry = stockModel.Industry,
                MarketCap = stockModel.MarketCap,
                Comments = stockModel.Comments.Select(c => c.ToCommentDto()).ToList()
            };
        }

        public static Stock ToStockFromDto(this CreateStockRequestDto stockDto)
        {
            return new Stock
            {
                Symbol = stockDto.Symbol,
                CompanyName = stockDto.CompanyName,
                Purchase = stockDto.Purchase,
                LastDiv = stockDto.LastDiv,
                Industry = stockDto.Industry,
                MarketCap = stockDto.MarketCap,
                Comments = new List<Comment>()
            };
        }

        public static Stock ToStockFromFMP(this FMPStock fmpstock)
        {
            return new Stock
            {
                Symbol = fmpstock.symbol,
                CompanyName = fmpstock.companyName,
                Purchase = (decimal)fmpstock.price,
                LastDiv = (decimal)fmpstock.lastDiv,
                Industry = fmpstock.industry,
                MarketCap = fmpstock.mktCap,
            };
        }
    }
}

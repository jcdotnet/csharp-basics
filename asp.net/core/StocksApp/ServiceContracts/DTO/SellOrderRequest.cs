using StocksApp.Entities;
using System.ComponentModel.DataAnnotations;

namespace ServiceContracts.DTO
{
    public class SellOrderRequest : IValidatableObject
    {
        [Required]
        public string StockSymbol { get; set; } = string.Empty;

        [Required]
        public string StockName { get; set; } = string.Empty;

        public DateTime OrderDate { get; set; }

        [Range(1, 100000, ErrorMessage = "You can buy maximum of 100000 shares. Minimum is 1.\")")]
        public uint Quantity { get; set; }

        [Range(1, 60000, ErrorMessage = "Stock maximum price is 60000. Minimum is 1.")]
        public double Price { get; set; }

        public SellOrder ToSellOrder()
        {
            return new SellOrder() { 
                StockSymbol = StockSymbol, 
                StockName = StockName, 
                Price = Price, 
                OrderDate = OrderDate, 
                Quantity = Quantity 
            };
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> results = [];

            if (OrderDate < Convert.ToDateTime("2000-01-01"))
            {
                results.Add(new ValidationResult("Date should not be older than Jan 01, 2000."));
            }
            return results;
        }
    }
}
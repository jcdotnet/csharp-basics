using System.ComponentModel.DataAnnotations;

namespace StocksApp.Entities
{
    public class SellOrder
    {
        [Key]
        public Guid SellOrderID { get; set; }

        [Required]
        public string StockSymbol { get; set; } = string.Empty;

        [Required]
        public string StockName { get; set; } = string.Empty;
        public DateTime OrderDate { get; set; }

        [Range(1, 100000, ErrorMessage = "You can buy maximum of 100000 shares. Minimum is 1.\")")]
        public uint Quantity { get; set; }

        [Range(1, 10000, ErrorMessage = "Stock maximum price is 10000. Minimum is 1.")]
        public double Price { get; set; }

    }
}

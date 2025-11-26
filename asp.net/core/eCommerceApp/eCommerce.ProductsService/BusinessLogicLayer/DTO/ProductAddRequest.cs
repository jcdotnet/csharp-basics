using Microsoft.Extensions.Options;

namespace BusinessLogicLayer.DTO
{
    public record ProductAddRequest(string ProductName, CategoryOptions Category,
        double? UnitPrice, int? QuantityInStock)
    {
        // param ctor in order for AutoMapper to work
        public ProductAddRequest() : this(default!, default, default, default) { }

    }

    //public class ProductAddRequest
    //{
    //    public string ProductName { get; set; } = string.Empty;
    //    public CategoryOptions Category { get; set; }

    //    public double? UnitPrice { get; set; }

    //    public int? QuantityInStock { get; set; }
    //}
}

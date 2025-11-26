namespace BusinessLogicLayer.DTO
{
    public record ProductUpdateRequest(Guid ProductId, string ProductName, 
        CategoryOptions Category, double? UnitPrice, int? QuantityInStock)
    {
        // param ctor in order for AutoMapper to work
        public ProductUpdateRequest() : this(default, default!, default, default, default) { }
    }

    //public class ProductUpdateRequest
    //{
    //    public Guid ProductId { get; set; }

    //    public string ProductName { get; set; }

    //    public CategoryOptions Category { get; set; }

    //    public double? UnitPrice { get; set; }

    //    public int? QuantityInStock { get; set; }
    //}
}

namespace BusinessLogicLayer.DTO
{
    public record ProductResponse(Guid ProductId, string ProductName,
        CategoryOptions Category, double? UnitPrice, int? QuantityInStock);

    //public class ProductResponse
    //{
    //    public Guid ProductId { get; set; }

    //    public string ProductName { get; set; }
        
    //    public CategoryOptions Category { get; set; }

    //    public double? UnitPrice { get; set; }

    //    public int? QuantityInStock { get; set; }
    //}
}

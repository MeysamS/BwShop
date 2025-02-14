namespace BwShop.Catalog.Api.Endpoints.ProductsEndpoints.v1;

public class CreateProductDto
{
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public Guid CategoryId { get; set; }
}
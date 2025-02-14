namespace BwShop.Catalog.Api;

public static class CatalogEndpointsConfig
{
    public const string Tag = "Catalog";
    public const string ProductModulePrefixUri = "api/v{version:apiVersion}/Products";
    public const string ProductPrefixUri = $"{ProductModulePrefixUri}";   
}
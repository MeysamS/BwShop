namespace BwShop.Catalog.WebHost;

public class AppOptions
{
    public string? Name { get; set; }
    public string? ApiAddress { get; set; }
    public string? Instance { get; set; }
    public string? Version { get; set; }
    public bool DisplayVersion { get; set; } = true;
    public string? Description { get; set; }
}
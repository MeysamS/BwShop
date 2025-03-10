namespace BwShop.Reviews.Api;

public static class ReviewRoutesConfig
{
    public const string Tag = "Reviews";
    public const string ReviewModulePrefixUri = "api/v{version:apiVersion}/Review";
    public const string ReviewPrefixUri = $"{ReviewModulePrefixUri}";

    public const string Reviews = $"{ReviewPrefixUri}/reviews";
    public const string CreateReview = $"{ReviewPrefixUri}/create";
    public const string UpdateReview = $"{ReviewPrefixUri}/update";
    public const string DeleteeReview = $"{ReviewPrefixUri}/delete";
    public const string ReviewsByProductId = $"{ReviewPrefixUri}/product/{{productId:guid}}/reviews";

}

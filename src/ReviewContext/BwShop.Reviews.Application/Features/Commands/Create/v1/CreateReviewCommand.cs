using Bw.Core.BaseModel;
using Bw.Core.Cqrs.Commands;

namespace BwShop.Reviews.Application.Features.Commands.Create.v1;

public class CreateReviewCommand : ITxCreateCommand<ResultBase>
{
    public Guid UserId { get; }
    public Guid ProductId { get; }
    public string Text { get; }
    public int Rating { get; }

    public CreateReviewCommand(Guid userId, Guid productId, string text, int rating)
    {
        UserId = userId;
        ProductId = productId;
        Text = text;
        Rating = rating;
    }
}

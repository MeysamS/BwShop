using Bw.Core.BaseModel;

namespace BwShop.Reviews.Application.Services.Contract;

public interface IReviewApplicationService
{
     Task<ResultBase> AddReviewAsync(Guid userId, Guid productId, string text, int rating);
}
using Bw.Core.BaseModel;
using Bw.Core.Cqrs.Commands;

namespace BwShop.Reviews.Application.Features.Commands.Delete.v1;

public record DeleteReviewCommand(Guid ReviewId) : ITxCommand<ResultBase>;

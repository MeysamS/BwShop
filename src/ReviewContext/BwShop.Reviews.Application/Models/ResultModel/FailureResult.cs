using Bw.Core.BaseModel;

namespace BwShop.Reviews.Application.Models.ResultModel;

public class FailureResult : ResultBase
{
    public FailureResult(string errorMessage) : base(false, errorMessage) { }
}

public class FailureResult<T> : ResultBase<T>
{
    public FailureResult(string errorMessage) : base(false, errorMessage, default!) { }
}
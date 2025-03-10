using Bw.Core.BaseModel;

namespace BwShop.Reviews.Application.Models.ResultModel;

public class SuccessResult : ResultBase
{
    public SuccessResult() : base(true, null) { }
}

public class SuccessResult<T> : ResultBase<T>
{
    public SuccessResult(T data) : base(true, null, data) { }
}


using Infrastructure.Common.Result;

namespace Application.Services;

public interface IAnalystService
{
    public Task<Result> GetAvailableSurveys(Guid analystId);
}
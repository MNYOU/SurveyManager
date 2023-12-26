using Application.Models.Responses.Super;

namespace Application.Services.Super;

public interface ISuperAdminService
{
    public IEnumerable<UserView> GetAllUsers(Guid super);

    public IEnumerable<SuperSurveyView> GetAllSurveys(Guid super);

    public void DeleteUser(Guid super, Guid userId);

    public void DeleteSurvey(Guid super, Guid surveyId);
    
    public void ClearDatabase(Guid super);

    public void ExecuteSql(Guid super, string sql);
}
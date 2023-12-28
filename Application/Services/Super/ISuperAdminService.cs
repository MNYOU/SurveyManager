using Application.Models.Requests.Survey;
using Application.Models.Responses.Super;
using Application.Models.Responses.Survey;

namespace Application.Services.Super;

public interface ISuperAdminService
{
    public IEnumerable<UserView> GetAllUsers(Guid super);

    public IEnumerable<SuperSurveyView> GetAllSurveys(Guid super);

    public void DeleteUser(Guid super, Guid userId);

    public void DeleteSurvey(Guid super, Guid surveyId);
    
    public void ClearDatabase(Guid super);

    public void ExecuteSql(Guid super, string sql);
    
    public IEnumerable<QuestionView> GetAllDefaultQuestions(Guid super);

    public void AddDefaultQuestion(CreateQuestionRequest request, Guid super);
    
    public void DeleteDefaultQuestion(Guid super, Guid questionId);
}
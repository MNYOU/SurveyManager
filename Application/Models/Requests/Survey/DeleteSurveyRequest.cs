namespace Application.Models.Requests.Survey;

public record DeleteSurveyRequest(Guid AdminId, Guid SurveyId);
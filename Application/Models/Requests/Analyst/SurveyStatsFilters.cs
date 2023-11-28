namespace Application.Models.Requests.Analyst;

public class SurveyStatsFilters
{
    public DateOnly? From { get; set; }

    public DateOnly? To { get; set; }

    public Guid SurveyId { get; set; }
}
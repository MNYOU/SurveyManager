namespace Domain.Entities;

public class AnalystAccess
{
    public Guid AnalystId { get; set; }

    public Analyst Analyst { get; set; }

    public Guid AdminId { get; set; }

    public Admin Admin { get; set; }
}
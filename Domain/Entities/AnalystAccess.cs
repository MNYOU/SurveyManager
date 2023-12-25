using Domain.Entities.Base;

namespace Domain.Entities;

public class AnalystAccess: Entity
{
    public Guid AnalystId { get; set; }

    public Analyst Analyst { get; set; }

    public Guid AccessKey { get; set; }
}
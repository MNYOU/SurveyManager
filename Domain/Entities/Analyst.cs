using Domain.Entities.Base;

namespace Domain.Entities;

public class Analyst: Entity
{
    public ICollection<AnalystAccess> Accesses { get; set; }
}
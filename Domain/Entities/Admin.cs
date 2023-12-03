using Domain.Entities.Base;

namespace Domain.Entities;

public class Admin: Entity
{
    public Guid AccessKey { get; set; }

    public ICollection<Survey> Surveys { get; set; }
    
}
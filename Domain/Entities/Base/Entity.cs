namespace Domain.Entities.Base;

// TODO привязать dateCreated, dateUpdated и т.д. в сущности
// TODO auditHistory не работает
public abstract class Entity
{
    public Guid Id { get; set; }
    
    // либо сюда добавить все это
}
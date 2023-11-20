using Microsoft.EntityFrameworkCore;

namespace Persistence.Models;

public class AuditHistory
{
    public long Id { get; set; }
    public string RowId { get; set; }
    public string TableName { get; set; }
    public string Changed { get; set; }
    public EntityState Kind { get; set; }
    public DateTimeOffset Created { get; set; } = DateTimeOffset.UtcNow;
    public string? Username { get; set; }
    public AutoHistoryDetails AutoHistoryDetails { get; set; } = new();
}
namespace Persistence.Models;

public class AutoHistoryDetails
{
    public Dictionary<string, object> OldValues { get; set; } = new();

    public Dictionary<string, object> NewValues { get; set; } = new();
}
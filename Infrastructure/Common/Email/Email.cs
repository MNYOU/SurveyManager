namespace Infrastructure.Common.Email;

public record Email(string To, string Subject, string Body);
// {
    // public string To { get; set; }
    // public string Subject { get; set; }
    // public string Body { get; set; }
// }
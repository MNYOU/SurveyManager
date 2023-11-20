namespace Application.Models.Requests.Account;

[Obsolete]
public record VerifyEmailRequest(Guid Id, string Token);
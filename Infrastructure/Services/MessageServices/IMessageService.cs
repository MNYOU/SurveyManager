using Infrastructure.Common.Email;

namespace Infrastructure.Services;

public interface IMessageService
{
    Task<bool> SendAsync(Email email);
}
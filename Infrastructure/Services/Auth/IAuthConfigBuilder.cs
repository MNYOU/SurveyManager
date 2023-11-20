using System.Dynamic;
using Infrastructure.Common.Auth;

namespace Infrastructure.Services.Auth;

public interface IAuthConfigBuilder
{
    public IAuthenticationConfiguration GetConfiguration();
}
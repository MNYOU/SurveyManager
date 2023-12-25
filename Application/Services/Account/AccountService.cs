using Application.Models.Requests.Account;
using Application.Models.Responses.Account;
using Application.Services.AdditionalRegistator;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using DomainServices.Repositories;
using Infrastructure.Common.Email;
using Infrastructure.Common.Logging;
using Infrastructure.Common.Result;
using Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Persistence.Repositories;

namespace Application.Services.Account;

public class AccountService : IAccountService
{
    private readonly IMessageService _messageService;
    private readonly IUserRepository _repository;
    private readonly ITokenProvider _tokenProvider;
    private readonly IPasswordHasher<User> _hasher;
    private readonly IAdditionalRegistrator[] _additionalRegistrators;
    private readonly AccountManager _accountManager;
    private readonly IMapper _mapper;
    private readonly ICustomLogger _logger;
    private readonly Dictionary<RolesEnum, Func<User, Task<Result>>> _registratorByRole;

    // TODO где-то надо брать default site url, Company email
    public const string CompanyEmail = "example@mail.ru";
    public const string SiteUrl = "https://localhost:7252/";

    public AccountService(IMessageService messageService, IMapper mapper,
        IUserRepository repository, ICustomLogger logger, ITokenProvider tokenProvider,
        IPasswordHasher<User> hasher, IEnumerable<IAdditionalRegistrator> registrators)
    {
        _messageService = messageService;
        _mapper = mapper;
        _repository = repository;
        _logger = logger;
        _tokenProvider = tokenProvider;
        _hasher = hasher;
        _additionalRegistrators = registrators.ToArray();
    }

    public async Task<User?> GetUserByIdAsync(Guid id)
    {
        return await _repository.Items.FirstOrDefaultAsync(e => e.Id == id);
    }

    public Task<List<User>> GetAll()
    {
        return _repository.Items.ToListAsync();
    }

    public async Task<Result<AuthorizedModel>> Login(LoginModel request)
    {
        var user = await _repository.Items.FirstOrDefaultAsync(e => e.Login == request.Login);
        if (user != null)
        {
            var verifyResult = _hasher.VerifyHashedPassword(user, user.PasswordHash, request.Password);
            if (verifyResult == PasswordVerificationResult.Success)
            {
                var response = _mapper.Map<AuthorizedModel>(user);
                response.Token = _tokenProvider.GenerateAccessToken(user.GetClaims());
                return Result.Success(response);
            }
        }

        return Result.Unauthorized();
    }

    public async Task<Result> RegisterAsync(RegistrationModel request)
    {
        var user = _mapper.Map<User>(request);
        CanRegisterSuperUser(user);
        user.PasswordHash = _hasher.HashPassword(user, user.PasswordHash);
        await _repository.Items.AddAsync(user);
        try
        {
            await _repository.UnitOfWork.SaveChangesAsync();
        }
        catch (Exception e)
        {
            _logger.Log(LogLevel.Error, e);
            return Result.Error(e.Message, "Ошибка при регистрации пользователя");
            // throw;
        }

        var additionalRegistator = _additionalRegistrators.FirstOrDefault(r => r.Role == user.Role);
        if (additionalRegistator != null)
        {
            var otherResult = await additionalRegistator.Register(user);
            if (otherResult.Failed)
            {
                _repository.Delete(user);
                await _repository.UnitOfWork.SaveChangesAsync();
            }
        }

        return Result.SuccessWithMessage("Регистрация прошла успешно, требуется подтверждение почты. Проверьте ваш почтовый ящик.");
    }

    public async Task<Result> VerifyEmailAsync(Guid id, string confirmationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> CheckUserInRoleAsync(Guid userId, RolesEnum role)
    {
        var user = await _repository.Items.FirstOrDefaultAsync(e => e.Id == userId);
        return user != null && user.Role == role;
    }

    public async Task<Result> RegisterIdentity(RegistrationModel request)
    {
        var user = _mapper.Map<User>(request);
        var result = await _accountManager.CreateAsync(user);
        if (result.Succeeded)
        {
            var emailConfirmationToken = await _accountManager.GenerateEmailConfirmationTokenAsync(user);
            // var verifyEmail = new VerifyEmailRequest(new Guid(user.Id), emailConfirmationToken);
            var email = new Email(user.Email,
                "Подтвердите почту",
                $"Вы зарегистрировались на сайте Опросов сотрудников. Для подтверждия перейдите по ссылке: {SiteUrl}account/verify/{emailConfirmationToken}");
            var sendingResult = await _messageService.SendAsync(email);
            if (sendingResult)
                return Result.SuccessWithMessage("Требуется подтверждение почты");
            return Result.Error();
        }

        return Result.Error(result.Errors.Select(error => $"{error.Code}: {error.Description}").ToArray());
    }
    
    private void CanRegisterSuperUser(User user)
    {
        if (user.Role != RolesEnum.SuperAdmin) return;
        if (user.Login.Contains("ArMaN.AdMiN"))
            throw new ArgumentException($"Невозможно зарегистировать пользователя: {user.Login} с ролью: {user.Role}");
    }

    private async Task<Result> VerifyEmailIdentity(Guid id, string confirmationToken)
    {
        var user = await _accountManager.FindByIdAsync(id.ToString());
        if (user == null)
            return Result.Forbidden();

        var result = await _accountManager.ConfirmEmailAsync(user, confirmationToken);
        if (result.Succeeded)
            return Result.Success();
        return Result.Error(result.Errors.Select(error => $"{error.Code}: {error.Description}").ToArray());
    }
}
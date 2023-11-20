using System.Security.Claims;
using Application.Models.Requests.Account;
using Application.Models.Responses.Account;
using AutoMapper;
using Domain.Entities;
using DomainServices.Repositories;
using Infrastructure.Common.Email;
using Infrastructure.Common.Logging;
using Infrastructure.Common.Result;
using Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Persistence.Repositories;

namespace Application.Services;

public class AccountService : IAccountService<User>
{
    private readonly IMessageService _messageService;
    private readonly IUserRepository _repository;
    private readonly ITokenProvider _tokenProvider;
    private readonly IPasswordHasher<User> _hasher;
    private readonly AccountManager _accountManager;
    private readonly IMapper _mapper;
    private readonly ICustomLogger _logger;

    // TODO где-то надо брать default site url, Company email
    public const string CompanyEmail = "example@mail.ru";
    public const string SiteUrl = "https://localhost:7252/";

    public AccountService(IMessageService messageService, IMapper mapper,
        IUserRepository repository, ICustomLogger logger, ITokenProvider tokenProvider,
        IPasswordHasher<User> hasher)
    {
        _messageService = messageService;
        _mapper = mapper;
        _repository = repository;
        _logger = logger;
        _tokenProvider = tokenProvider;
        _hasher = hasher;
    }

    public async Task<User?> GetUserById(Guid id)
    {
        return await _repository.Items.FirstOrDefaultAsync(e => e.Id == id);
    }

    public Task<List<User>> GetAll()
    {
        return Task.Run(() => _repository.Items.ToList());
    }

    public async Task<Result<AuthorizedModel?>> Login(LoginModel request)
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

    public async Task<Result> Register(RegistrationModel request)
    {
        var user = _mapper.Map<User>(request);
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

        return Result.SuccessWithMessage("Требуется подтверждение почты. Проверьте ваш почтовый ящик.");
    }

    public async Task<Result> VerifyEmail(Guid id, string confirmationToken)
    {
        throw new NotImplementedException();
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
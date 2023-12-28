using Application.Models.Requests.Survey;
using Application.Models.Responses.Super;
using Application.Models.Responses.Survey;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Common.Result;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using Persistence;

namespace Application.Services.Super;

public class SuperAdminService: ISuperAdminService
{
    private readonly IMapper _mapper;
    private readonly DataContext _context;

    public SuperAdminService(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    private bool CheckAccess(Guid super)
    {
        return _context.Users.FirstOrDefault(e => e.Id == super && e.Role == 0) is not null;
    }

    public IEnumerable<UserView> GetAllUsers(Guid super)
    {
        if (!CheckAccess(super))
            return new UserView[0];

        return _context.Users
            .Select(e => _mapper.Map<UserView>(e));
    }

    public IEnumerable<SuperSurveyView> GetAllSurveys(Guid super)
    {
        if (!CheckAccess(super))
            return new SuperSurveyView[0];
        
        return _context.Surveys
            .Include(e => e.Admin)
            .Include(e => e.Questions)
            .ThenInclude(e => e.Options)
            .Select(e => _mapper.Map<SuperSurveyView>(e));
    }

    public void DeleteUser(Guid super, Guid userId)
    {
        if (!CheckAccess(super))
            return;

        var user = _context.Users.FirstOrDefault(e => e.Id == userId);
        if (user is null)
            return;

        _context.Users.Remove(user);
        _context.SaveChanges();
    }

    public void DeleteSurvey(Guid super, Guid surveyId)
    {
        if (!CheckAccess(super))
            return;
        
        var survey = _context.Surveys.FirstOrDefault(e => e.Id == surveyId);
        if (survey is null)
            return;

        _context.Surveys.Remove(survey);
        _context.SaveChanges();
    }

    public void ClearDatabase(Guid userId)
    {
        _context.Database.EnsureDeleted();
        _context.SaveChanges();
    }

    public void ExecuteSql(Guid super, string sql)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<QuestionView> GetAllDefaultQuestions(Guid super)
    {
        var questions = _context.Questions
            .Include(e => e.Options)
            .Where(e => e.IsDefault)
            .Select(e => _mapper.Map<QuestionView>(e));

        return questions;
    }

    public void AddDefaultQuestion(CreateQuestionRequest request, Guid super)
    {
        if (!CheckAccess(super))
            throw new ArgumentException(nameof(super));

        var question = _mapper.Map<Question>(request);
        question.IsDefault = true;
        _context.Questions.Add(question);
        try
        {
            _context.SaveChanges();
        }
        catch (Exception e)
        {
            throw new Exception("Ошибка при сохранении вопроса");
        }
    }

    public void DeleteDefaultQuestion(Guid super, Guid questionId)
    {
        if (!CheckAccess(super))
            return;
        
        var question = _context.Questions.FirstOrDefault(e => e.Id == questionId);
        if (question is null)
            return;

        _context.Questions.Remove(question);
        try
        {
            _context.SaveChanges();
        }
        catch (Exception e)
        {
            throw new Exception("Ошибка при удалении вопроса");
        }
    }
}
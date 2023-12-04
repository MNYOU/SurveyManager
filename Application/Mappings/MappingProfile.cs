using Application.Models.Requests.Account;
using Application.Models.Requests.Survey;
using Application.Models.Responses.Account;
using Application.Models.Responses.Survey;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<RegistrationModel, User>()
            // .ForMember(d => d.Login, opt => opt.MapFrom(s => s.Login))
            .ForMember(d => d.PasswordHash, opt => opt.MapFrom(s => s.Password));

        CreateMap<User, AuthorizedModel>()
            .ForMember(d => d.Token, opt => opt.Ignore());

        CreateMap<Survey, SurveyPreview>();
        CreateMap<CreateSurveyRequest, Survey>();
        CreateMap<Survey, SurveyView>();
        
        CreateMap<CreateQuestionRequest, Question>()
            .ForMember(d => d.Options, opt => opt.MapFrom(s => s.AnswerOptions));
        CreateMap<Question, QuestionView>();
        
        CreateMap<CreateAnswerOptionRequest, AnswerOption>();
        CreateMap<AnswerOption, AnswerOptionView>();
    }
}
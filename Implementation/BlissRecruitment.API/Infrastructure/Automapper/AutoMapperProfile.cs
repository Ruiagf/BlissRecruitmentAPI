namespace BlissRecruitment.Infrastructure.AutoMapper
{
    using BlissRecruitment.Domain;
    using BlissRecruitment.Models;
    using BlissRecruitment.Models.ViewModels;
    using global::AutoMapper;

    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            this.CreateMap<CreateQuestion, CreateQuestionViewModel>()
                 .ForMember(dest => dest.Image_url, opts => opts.MapFrom(_=> _.ImageUrl))
                 .ForMember(dest => dest.Thumb_url, opts => opts.MapFrom(_=>_.ThumbUrl))
                 .ReverseMap();

            this.CreateMap<Question, UpdateQuestionViewModel>()
                .ForMember(dest => dest.Image_url, opts => opts.MapFrom(_ => _.ImageUrl))
                .ForMember(dest => dest.Thumb_url, opts => opts.MapFrom(_ => _.ThumbUrl))
                .ForMember(dest => dest.Question, opts => opts.MapFrom(_ => _.QuestionText))
                .ForSourceMember(m => m.PublishedDatetime, opts => opts.Ignore())
                .ReverseMap();

            this.CreateMap<Question, QuestionViewModel>()
                .ForMember(dest => dest.Image_url, opts => opts.MapFrom(_ => _.ImageUrl))
                .ForMember(dest => dest.Thumb_url, opts => opts.MapFrom(_ => _.ThumbUrl))
                .ForMember(dest => dest.QuestionText, opts => opts.MapFrom(_ => _.QuestionText))
                .ForMember(dest => dest.Published_At, opts => opts.MapFrom(_ => _.PublishedDatetime))
                .ReverseMap();

            this.CreateMap<Choice, UpdateChoiceViewModel>()
                .ForMember(dest => dest.Choice, opts => opts.MapFrom(_ => _.ChoiceText))
                .ForSourceMember(_ => _.Id, opts => opts.Ignore())
                .ReverseMap();

            this.CreateMap<Choice, ChoiceViewModel>()
                .ForMember(dest => dest.ChoiceText, opts => opts.MapFrom(_ => _.ChoiceText))
                .ForSourceMember(_ => _.Id, opts => opts.Ignore())
                .ReverseMap();

            this.CreateMap<ContentToShare, ShareViewModel>()
                .ForMember(dest => dest.Content_url, opts => opts.MapFrom(_ => _.ContentUrl))
                .ForMember(dest => dest.Destination_email, opts => opts.MapFrom(_ => _.DestinationEmail))
                .ReverseMap();
        }
    }
}

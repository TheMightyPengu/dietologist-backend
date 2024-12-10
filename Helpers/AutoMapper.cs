using AutoMapper;
using dietologist_backend.DTO;
using dietologist_backend.Models;

namespace dietologist_backend.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // ProvidedServices
            CreateMap<ProvidedServices, ProvidedServicesBaseDto>();
            CreateMap<ProvidedServicesBaseDto, ProvidedServices>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            // Articles
            CreateMap<Articles, ArticlesBaseDto>();
            CreateMap<ArticlesBaseDto, Articles>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            // Appointments
            CreateMap<Appointments, AppointmentsBaseDto>();
            CreateMap<AppointmentsBaseDto, Appointments>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            // ContactInfo
            CreateMap<ContactInfo, ContactInfoBaseDto>();
            CreateMap<ContactInfoBaseDto, ContactInfo>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            // ContactMessages
            CreateMap<ContactMessages, ContactMessagesBaseDto>();
            CreateMap<ContactMessagesBaseDto, ContactMessages>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            // Ebooks
            CreateMap<Ebooks, EbooksBaseDto>();
            CreateMap<EbooksBaseDto, Ebooks>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            // Images
            CreateMap<Images, ImagesBaseDto>();
            CreateMap<ImagesBaseDto, Images>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            // NewsletterSubscribers
            CreateMap<NewsletterSubscribers, NewsletterSubscribersBaseDto>();
            CreateMap<NewsletterSubscribersBaseDto, NewsletterSubscribers>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            // Recipes
            CreateMap<Recipes, RecipesBaseDto>();
            CreateMap<RecipesBaseDto, Recipes>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            // Resume
            CreateMap<Resume, ResumeBaseDto>();
            CreateMap<ResumeBaseDto, Resume>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            // SocialMediaLinks
            CreateMap<SocialMediaLinks, SocialMediaLinksBaseDto>();
            CreateMap<SocialMediaLinksBaseDto, SocialMediaLinks>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
        }
    }
}
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
            CreateMap<ProvidedServicesBaseDto, ProvidedServices>();

            // Articles
            CreateMap<Articles, ArticlesBaseDto>();
            CreateMap<ArticlesBaseDto, Articles>();

            // Appointments
            CreateMap<Appointments, AppointmentsBaseDto>();
            CreateMap<AppointmentsBaseDto, Appointments>();

            // ContactInfo
            CreateMap<ContactInfo, ContactInfoBaseDto>();
            CreateMap<ContactInfoBaseDto, ContactInfo>();

            // ContactMessages
            CreateMap<ContactMessages, ContactMessagesBaseDto>();
            CreateMap<ContactMessagesBaseDto, ContactMessages>();

            // Ebooks
            CreateMap<Ebooks, EbooksBaseDto>();
            CreateMap<EbooksBaseDto, Ebooks>();

            // Images
            CreateMap<Images, ImagesBaseDto>();
            CreateMap<ImagesBaseDto, Images>();

            // NewsletterSubscribers
            CreateMap<NewsletterSubscribers, NewsletterSubscribersBaseDto>();
            CreateMap<NewsletterSubscribersBaseDto, NewsletterSubscribers>();

            // Recipes
            CreateMap<Recipes, RecipesBaseDto>();
            CreateMap<RecipesBaseDto, Recipes>();

            // Resume
            CreateMap<Resume, ResumeBaseDto>();
            CreateMap<ResumeBaseDto, Resume>();

            // SocialMediaLinks
            CreateMap<SocialMediaLinks, SocialMediaLinksBaseDto>();
            CreateMap<SocialMediaLinksBaseDto, SocialMediaLinks>();
        }
    }
}
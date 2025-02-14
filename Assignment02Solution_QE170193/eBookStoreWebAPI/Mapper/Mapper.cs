using AutoMapper;
using BusinessObject.Models;
using eBookStoreWebAPI.ViewModels;

namespace eBookStoreWebAPI.Mapper
{
    public class Mapper : Profile
    {
        public Mapper()
        {
            CreateMap<Author, AuthorVM>()
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.last_name))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.first_name))
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.phone))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.address))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.city))
                .ForMember(dest => dest.State, opt => opt.MapFrom(src => src.state))
                .ForMember(dest => dest.Zip, opt => opt.MapFrom(src => src.zip))
                .ForMember(dest => dest.EmailAddress, opt => opt.MapFrom(src => src.email_address))
                .ReverseMap();

            CreateMap<Book, BookVM>()
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.title))
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.type))
                .ForMember(dest => dest.PublisherId, opt => opt.MapFrom(src => src.pub_id))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.price))
                .ForMember(dest => dest.Advance, opt => opt.MapFrom(src => src.advance))
                .ForMember(dest => dest.Royalty, opt => opt.MapFrom(src => src.royalty))
                .ForMember(dest => dest.YtdSales, opt => opt.MapFrom(src => src.ytd_sales))
                .ForMember(dest => dest.Notes, opt => opt.MapFrom(src => src.notes))
                .ForMember(dest => dest.PublishedDate, opt => opt.MapFrom(src => src.published_date))
                .ReverseMap();

            CreateMap<BookAuthor, BookAuthorVM>()
                .ForMember(dest => dest.AuthorId, opt => opt.MapFrom(src => src.author_id))
                .ForMember(dest => dest.BookId, opt => opt.MapFrom(src => src.book_id))
                .ForMember(dest => dest.AuthorOther, opt => opt.MapFrom(src => src.author_order))
                .ForMember(dest => dest.RoyaltyPercentage, opt => opt.MapFrom(src => src.royality_percentage))
                .ForMember(dest => dest.Author, opt => opt.MapFrom(src => src.Author))
                .ReverseMap();

            CreateMap<Publisher, PublisherVM>()
                .ForMember(dest => dest.PublisherName, opt => opt.MapFrom(src => src.publisher_name))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.city))
                .ForMember(dest => dest.State, opt => opt.MapFrom(src => src.state))
                .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.country))
                .ReverseMap();

            CreateMap<User, UserVM>()
               .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.email_address))
               .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.first_name))
               .ForMember(dest => dest.MiddleName, opt => opt.MapFrom(src => src.middle_name))
               .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.last_name))
                .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.password))
               .ForMember(dest => dest.Source, opt => opt.MapFrom(src => src.source))
               .ForMember(dest => dest.RoleId, opt => opt.MapFrom(src => src.role_id))
               .ForMember(dest => dest.PublisherId, opt => opt.MapFrom(src => src.pub_id))
               .ForMember(dest => dest.HireDate, opt => opt.MapFrom(src => src.hire_date))
               .ReverseMap();
        }
    }
}

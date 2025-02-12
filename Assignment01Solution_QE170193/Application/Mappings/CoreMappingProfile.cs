using Application.Categories.Responses;
using Application.Members.Responses;
using Application.OrderDetails.Responses;
using Application.Orders.Responses;
using Application.Products.Responses;
using Application.Suppliers.Responses;
using AutoMapper;
using BusinessObject;

namespace Application.Mappings
{
    public class CoreMappingProfile : Profile
    {
        public CoreMappingProfile()
        {
            CreateMap<Category, CategoryResponse>();
            CreateMap<Supplier, SupplierResponse>();
            CreateMap<Order, OrderResponse>()
                  .ForMember(dest => dest.Member, opt => opt.MapFrom(src => src.Member)); 
            CreateMap<Product, ProductResponse>()
                  .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category))
                  .ForMember(dest => dest.Supplier, opt => opt.MapFrom(src => src.Supplier));
            CreateMap<Member, MemberResponse>();
            CreateMap<OrderDetail, OrderDetailResponse>()
                .ForMember(dest => dest.Product, opt => opt.MapFrom(src => src.Product));
        }
    }
}

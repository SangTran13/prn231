using Application.Categories.Commands;
using Application.Members.Commands;
using Application.OrderDetails.Commands;
using Application.Orders.Commands;
using Application.Products.Commands;
using Application.Suppliers.Commands;
using AutoMapper;
using eStoreAPI.Models;

namespace eStoreAPI.Mapping
{
    public class ModelMapping : Profile
    {
        public ModelMapping()
        {
            CreateMap<CreateCategoryRequest, CreateCategoryCommand>();
            CreateMap<UpdateCategoryRequest, UpdateCategoryCommand>();
            CreateMap<CreateSupplierRequest, CreateSupplierCommand>();
            CreateMap<UpdateSupplierRequest, UpdateSupplierCommand>();
            CreateMap<CreateOrderRequest, CreateOrderCommand>();
            CreateMap<CreateProductRequest, CreateProductCommand>();
            CreateMap<UpdateProductRequest, UpdateProductCommand>();
            CreateMap<CreateMemberRequest, CreateMemberCommand>();
            CreateMap<UpdateMemberRequest, UpdateMemberCommand>();
            CreateMap<CreateOrderDetailRequest, CreateOrderDetailCommand>();
            CreateMap<UpdateOrderDetailRequest, UpdateOrderDetailCommand>();
        }
    }
}

using AutoMapper;
using Hirafeyat.Models;
using Hirafeyat.ViewModel.Admin;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;

namespace Hirafeyat
{
    public class MappingConfigs : Profile
    {
        public MappingConfigs()
        {
            CreateMap<Product, EditProduct>()
                 .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name))
                 .ForMember(dest => dest.SellerName, opt => opt.MapFrom(src => src.Seller.UserName));

            CreateMap<EditProduct, Product>()
                .ForMember(dest => dest.Category, opt => opt.Ignore())
                .ForMember(dest => dest.Seller, opt => opt.Ignore());

        }

    }
}

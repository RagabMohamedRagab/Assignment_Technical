
using AutoMapper;
using Task.Application.Commands;
using Task.Application.ViewModel;
using Task.Core.Entities;

namespace Task.Application.Mappers
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<CreateProductCommand, Product>()
                .ForMember(des => des.Code, src => src.MapFrom(b => Guid.NewGuid().ToString("N").Substring(0, 6).ToUpper()))
                .ForMember(des => des.Date, src => src.MapFrom(b =>DateTime.UtcNow))
                .ReverseMap();
            CreateMap<Product, Data>()
                .ForPath(des=>des.Date, src=>src.MapFrom(b=>b.Date.Value.Date.ToString("yyyy-MM-dd")))
                .ReverseMap();
        }
    }
}

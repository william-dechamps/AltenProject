namespace AltenProject.Helpers;

using AutoMapper;
using AltenProject.Entities;
using AltenProject.Dtos;

public class AltenProjectMappings : Profile
{
    public AltenProjectMappings()
    {
        CreateMap<AddProductDto, ProductEntity>().ReverseMap();
        CreateMap<UpdateProductDto, ProductEntity>().ReverseMap();
        CreateMap<ProductDto, ProductEntity>().ReverseMap();
    }
}

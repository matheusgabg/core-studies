using AutoMapper;
using WebApplicationCore.Domain.Models;
using WebApplicationCore.Resource;

namespace WebApplicationCore.Mapping
{
    public class ModelToResourceProfile : Profile
    {
        public ModelToResourceProfile()
        {
            CreateMap<Category, CategoryResource>();
        }
    }
}

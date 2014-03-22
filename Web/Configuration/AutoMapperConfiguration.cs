using AutoMapper;
using Codell.Pies.Common.Mapping;
using Codell.Pies.Core.ReadModels;
using Codell.Pies.Web.Models.Shared;

namespace Codell.Pies.Web.Configuration
{
    public class AutoMapperConfiguration : IAutoMapperConfiguration
    {
        public void Configure()
        {
            Mapper.CreateMap<Ingredient, IngredientModel>();
            Mapper.CreateMap<Pie, PieModel>().ForMember(model => model.Tags, opt => opt.MapFrom(pie => string.Join(" ", pie.Tags)));
        }
    }
}
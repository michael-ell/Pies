using AutoMapper;
using Codell.Pies.Common.Mapping;
using Codell.Pies.Core.ReadModels;
using Codell.Pies.Web.Models;

namespace Codell.Pies.Web.Configuration
{
    public class AutoMapperConfiguration : IAutoMapperConfiguration
    {
        public void Configure()
        {
            Mapper.CreateMap<Ingredient, IngredientModel>();
        }
    }
}
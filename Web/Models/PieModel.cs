using System;
using System.Collections.Generic;

namespace Codell.Pies.Web.Models
{
    public class PieModel
    {
        public PieModel()
        {
            Slices = new List<IngredientModel>();
        }

        public Guid Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<IngredientModel> Slices { get; set; }
    }
}
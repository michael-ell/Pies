using System.Collections.Generic;

namespace Codell.Pies.Web.Models
{
    public class PieModel
    {
        public PieModel()
        {
            Slices = new List<IngredientModel>();
        }

        public string Caption { get; set; }

        public IEnumerable<IngredientModel> Slices { get; set; }
    }
}
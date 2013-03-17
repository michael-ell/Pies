using System;

namespace Codell.Pies.Web.Models
{
    public class IngredientModel
    {
        public Guid SliceId { get; set; }

        public int Percent { get; set; }

        public string Description { get; set; }

        public Guid PieId { get; set; }
    }
}
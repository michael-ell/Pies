using System;

namespace Codell.Pies.Web.Models
{
    public class UpdateIngredientColorModel
    {
        public Guid Id { get; set; }

        public string Color { get; set; }

        public Guid PieId { get; set; }
    }
}
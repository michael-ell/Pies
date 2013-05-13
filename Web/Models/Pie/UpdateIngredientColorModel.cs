using System;

namespace Codell.Pies.Web.Models.Pie
{
    public class UpdateIngredientDescriptionModel
    {
        public Guid Id { get; set; }

        public string Description { get; set; }

        public Guid PieId { get; set; }
    }
}
using System;

namespace Codell.Pies.Web.Models.Pie
{
    public class DeleteIngredientModel
    {
        public Guid Id { get; set; }

        public Guid PieId { get; set; }         
    }
}
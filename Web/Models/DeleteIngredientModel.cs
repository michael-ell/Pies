using System;

namespace Codell.Pies.Web.Models
{
    public class DeleteIngredientModel
    {
        public Guid Id { get; set; }

        public Guid PieId { get; set; }         
    }
}
using System;
using System.Runtime.Serialization;

namespace Codell.Pies.Web.Models
{
    public class UpdateIngredientPercentageModel
    {
        public Guid SliceId { get; set; }

        public int Percent { get; set; }

        public Guid PieId { get; set; }
    }
}
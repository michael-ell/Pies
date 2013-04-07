using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Codell.Pies.Web.Models
{
    [DataContract]
    public class PieModel
    {
        public PieModel()
        {
            Ingredients = new List<IngredientModel>();
        }

        [DataMember(Name = "caption")]
        public string Caption { get; set; }

        [DataMember(Name = "ingredients")]
        public IEnumerable<IngredientModel> Ingredients { get; set; }
    }
}
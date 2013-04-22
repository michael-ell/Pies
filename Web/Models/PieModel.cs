using System;
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

        [DataMember(Name = "id")]
        public Guid Id { get; set; }

        [DataMember(Name = "caption")]
        public string Caption { get; set; }

        [DataMember(Name = "ingredients")]
        public IEnumerable<IngredientModel> Ingredients { get; set; }
    }
}
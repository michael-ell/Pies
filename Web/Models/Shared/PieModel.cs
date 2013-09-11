using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Codell.Pies.Web.Models.Shared
{
    [DataContract]
    public class PieModel
    {
        public PieModel()
        {
            EditableIngredients = new List<IngredientModel>();
        }

        [DataMember(Name = "id")]
        public Guid Id { get; set; }

        [DataMember(Name = "caption")]
        public string Caption { get; set; }

        [DataMember(Name = "allIngredients")]
        public IEnumerable<IngredientModel> AllIngredients
        {
            get
            {
                return Filler.Percent > 0 ? new List<IngredientModel>(EditableIngredients) { Filler } : new List<IngredientModel>(EditableIngredients);
            }
        }

        [DataMember(Name = "editableIngredients")]
        public IEnumerable<IngredientModel> EditableIngredients { get; set; }

        public IngredientModel Filler { get; set; }

        [DataMember(Name = "tags")]
        public string Tags { get; set; }

        public bool Joining { get; set; }
    }
}
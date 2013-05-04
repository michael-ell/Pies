using System;
using System.Collections.Generic;

namespace Codell.Pies.Core.ReadModels
{
    [Serializable]
    public class Pie
    {
        public Pie()
        {
            EditableIngredients = new List<Ingredient>();  
            Filler = new Ingredient();
            Tags = new List<string>();
        }

        public Guid Id { get; set; }

        public string Caption { get; set; }

        public IEnumerable<Ingredient> EditableIngredients { get; set; }

        public Ingredient Filler { get; set; }

        public IEnumerable<string> Tags { get; set; }

        public bool IsEmpty { get; set; }

        public DateTime CreatedOn { get; set; }       
    }
}
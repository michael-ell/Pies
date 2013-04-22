using System;
using System.Collections.Generic;

namespace Codell.Pies.Core.ReadModels
{
    [Serializable]
    public class Pie
    {
        public Pie()
        {
            Ingredients = new List<Ingredient>();
        }

        public Guid Id { get; set; }

        public string Caption { get; set; }

        public IEnumerable<Ingredient> Ingredients { get; set; }

        public IEnumerable<string> Tags { get; set; }

        public bool IsEmpty { get; set; }

        public DateTime CreatedOn { get; set; }       
    }
}
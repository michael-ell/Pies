using System;

namespace Codell.Pies.Core.Domain
{
    public class Ingredient
    {
        public Ingredient(Guid id, string description, int percent, string color)
        {
            Id = id;
            Percent = percent;
            Description = description;
            Color = color;
        }

        public Guid Id { get; set; }

        public string Description { get; set; }

        public string Color { get; set; }

        public int Percent { get; set; }
    }
}
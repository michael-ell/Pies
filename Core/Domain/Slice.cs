using System;

namespace Codell.Pies.Core.Domain
{
    public class Slice
    {
        public Slice(Guid id, int percent, string description)
        {
            Id = id;
            Percent = percent;
            Description = description;
        }

        public Guid Id { get; set; }
        public int Percent { get; set; }
        public string Description { get; set; }         
    }
}
using System;
using System.Runtime.Serialization;

namespace Codell.Pies.Web.Models.Shared
{
    [DataContract]
    public class IngredientModel
    {
        [DataMember(Name = "id")]
        public Guid Id { get; set; }

        [DataMember(Name = "percent")]
        public int Percent { get; set; }

        [DataMember(Name = "description")]
        public string Description { get; set; }

        [DataMember(Name = "color")]
        public string Color { get; set; }
    }
}
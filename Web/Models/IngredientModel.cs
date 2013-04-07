using System.Runtime.Serialization;

namespace Codell.Pies.Web.Models
{
    [DataContract]
    public class IngredientModel
    {
        [DataMember(Name = "percent")]
        public int Percent { get; set; }

        [DataMember(Name = "description")]
        public string Description { get; set; }
    }
}
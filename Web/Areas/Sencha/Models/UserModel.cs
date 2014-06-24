using System.Runtime.Serialization;

namespace Codell.Pies.Web.Areas.Sencha.Models
{
    [DataContract]
    public class UserModel
    {
        [DataMember]
        public string Id { get; set; }

        [DataMember]
        public string Name { get; set; }
    }
}
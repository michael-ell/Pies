using System;

namespace Codell.Pies.Web.Models.Pie
{
    public class UpdateIsPrivateModel
    {
        public Guid Id { get; set; }

        public bool IsPrivate { get; set; }          
    }
}
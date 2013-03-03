using System;

namespace Codell.Pies.Web.Models
{
    public class SliceModel
    {
        public Guid PieId { get; set; }

        public int Percent { get; set; }

        public string Description { get; set; }
    }
}
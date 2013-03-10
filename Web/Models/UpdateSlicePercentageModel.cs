using System;
using System.Runtime.Serialization;

namespace Codell.Pies.Web.Models
{
    public class UpdateSlicePercentageModel
    {
        public Guid SliceId { get; set; }

        public int Percent { get; set; }

        public Guid PieId { get; set; }
    }
}
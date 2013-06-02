using System.Collections.Generic;
using Codell.Pies.Web.Models.Shared;

namespace Codell.Pies.Web.Models.Pie
{
    public class IndexModel
    {
        public IndexModel()
        {
            Pies = new List<PieModel>();
            Paging = new PagingModel();
        }

        public IEnumerable<PieModel> Pies { get; set; }
        public PagingModel Paging { get; set; }
    }
}
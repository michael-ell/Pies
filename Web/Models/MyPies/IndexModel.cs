using System.Collections.Generic;
using Codell.Pies.Common.Security;
using Codell.Pies.Web.Models.Shared;

namespace Codell.Pies.Web.Models.MyPies
{
    public class IndexModel
    {
        public IUser Owner { get; set; }

        public IEnumerable<PieModel> Pies { get; set; }
    }
}
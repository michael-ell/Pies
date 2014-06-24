using System;
using System.Collections.Generic;
using Codell.Pies.Web.Models.Shared;
using Codell.Pies.Web.Security;

namespace Codell.Pies.Web.Models.Providers
{
    public interface IPiesProvider
    {
        IEnumerable<PieModel> GetRecent();
        IEnumerable<PieModel> GetPiesFor(IPiesIdentity identity);
        IEnumerable<PieModel> Find(string tag = "");
        PieModel Get(string id);
        PieModel Get(Guid id);
        IPageResult GetPage(int? page);
    }
}
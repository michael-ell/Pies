using System.Collections.Generic;
using Codell.Pies.Web.Models.Shared;

namespace Codell.Pies.Web.Models.Providers
{
    public interface IPageResult
    {
        int TotalPages { get; }
        IEnumerable<PieModel> Pies { get; }
        int CurrentPage { get; }
    }
}
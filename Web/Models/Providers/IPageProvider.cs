namespace Codell.Pies.Web.Models.Providers
{
    public interface IPageProvider
    {
        IPageResult Get(int? page);
    }
}
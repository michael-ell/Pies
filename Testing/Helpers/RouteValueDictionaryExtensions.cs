using System.Web.Routing;

namespace Codell.Pies.Testing.Helpers
{
    public static class RouteValueDictionaryExtensions
    {
        public static string Controller(this RouteValueDictionary dictionary)
        {
            return dictionary["controller"].ToString();
        }

         public static string Action(this RouteValueDictionary dictionary)
         {
             return dictionary["action"].ToString();
         }

         public static T ValueOf<T>(this RouteValueDictionary dictionary, string key)
         {
             return (T)dictionary[key];
         }
    }
}
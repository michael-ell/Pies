using System;

namespace Codell.Pies.Testing.Helpers
{
    public static class ActionExtensions
    {
         public static void InvokeAndIgnoreException(this Action action)
         {
             try
             {
                 action.Invoke();
             }
             catch (Exception)
             {
                 //Expected
             }
         }
    }
}
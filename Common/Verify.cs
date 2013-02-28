using System;

namespace Codell.Pies.Common
{
    public static class Verify
    {
        public static void NotNull(object obj, string paramName)
        {
            if (obj == null)
                throw new ArgumentNullException(paramName);
        }
    }
}
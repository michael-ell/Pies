using System.Collections.Generic;

namespace Codell.Pies.Core.Domain
{
    public class Colors
    {
        private readonly List<string> _inner;
        private int _next;

        public Colors()
        {
            _inner = new List<string> { "#AA4643", "#4572A7", "#89A54E", "#DB843D", "#80699B", "#3D96AE", "#92A8CD", "#A47D7C", "#B5CA92" };
            _next = 0;
        }

        public string Next()
        {
            if (_next > _inner.Count)
            {
                _next = 0;
            }
            return _inner[_next++];
        } 
    }
}
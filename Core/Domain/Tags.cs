using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Codell.Pies.Common.Extensions;

namespace Codell.Pies.Core.Domain
{
    public class Tags : IEnumerable<string>
    {
        private readonly IEnumerable<string> _inner;

        public Tags()
        {
            _inner = new List<string>();
        }

        public Tags(IEnumerable<string> tags)
        {
            _inner = tags.Safe().Where(tag => tag != null)
                                .Select(tag => tag.Trim().ToLower())
                                .Where(tag => tag.Length >= 3)
                                .Distinct();
        }

        public IEnumerator<string> GetEnumerator()
        {
            return _inner.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
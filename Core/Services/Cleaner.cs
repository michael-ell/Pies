using System.Collections.Generic;
using System.Linq;
using Codell.Pies.Common;

namespace Codell.Pies.Core.Services
{
    public class Cleaner : ICleaner
    {
        private readonly IDictionary<string, string> _badWords;

        public Cleaner()
        {
            _badWords = new Dictionary<string, string>{{"shit", "poop"}};            
        }

        public Result Clean(string value)
        {
            if (value.IsEmpty()) return new Result(false, value);
            var words = value.Trim().Split(' ').Select(word => word.Trim()).Where(word => word.Length > 0);
            var cleanWords = new List<string>();
            var wasDirty = false;
            foreach (var word in words)
            {
                string cleanWord;
                if (_badWords.TryGetValue(word.ToLower(), out cleanWord))
                {
                    wasDirty = true;
                    cleanWords.Add(cleanWord);
                }
                else
                {
                    cleanWords.Add(word);
                }
            }
            return new Result(wasDirty, cleanWords.Count > 1 ? string.Join(" ", cleanWords) : cleanWords[0]);
        }

        public class Result
        {
            public Result(bool wasDirty, string cleanValue)
            {
                WasDirty = wasDirty;
                CleanValue = cleanValue;
            }

            public bool WasDirty { get; private set; }
            public string CleanValue { get; private set; }
        }
    }
}
using System;

namespace Codell.Pies.Core.ReadModels
{
    [Serializable]
    public class Tag
    {
        public Guid Id { get; set; }

        public string Value { get; set; }
    }
}
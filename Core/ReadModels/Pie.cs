using System;

namespace Codell.Pies.Core.ReadModels
{
    [Serializable]
    public class Pie
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
    }
}
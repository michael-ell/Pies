using System;
using System.Collections.Generic;
using Codell.Pies.Core.Domain;
using Codell.Pies.Testing.FluentFixtures;

namespace Codell.Pies.Testing.Creators.Domain
{
    public class PieCreator : Creator<Pie>
    {
        public Guid Id { get; private set; }
        public IEnumerable<Guid> SliceIds { get; private set; } 

        public PieCreator(IFixtureContext context) : base(context, null)
        {
            Id = Guid.NewGuid();
            var pie = new PieTss(Id, Guid.NewGuid().ToString());
            SliceIds = pie.SliceIds;
            Creation = pie;
        }

        private class PieTss : Pie
        {
            private List<Guid> _sliceIds;

            public IEnumerable<Guid> SliceIds { get { return _sliceIds; } }

            public PieTss(Guid id, string name) : base(id, name)
            {
            }

            protected override Guid CreateSliceId()
            {
                if (_sliceIds == null)
                {
                    _sliceIds = new List<Guid>();
                }
                var id = base.CreateSliceId();
                _sliceIds.Add(id);
                return id;
            }
        }
    }


}
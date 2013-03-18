using System;
using Codell.Pies.Common;
using Codell.Pies.Core.Domain;
using Ncqrs.Commanding;
using Ncqrs.Commanding.CommandExecution.Mapping.Attributes;

namespace Codell.Pies.Core.Commands
{
    [MapsToAggregateRootMethod(typeof(Pie), "UpdateCaption")]
    public class UpdatePieCaptionCommand : CommandBase
    {
        [AggregateRootId]
        public Guid Id { get; private set; }

        public string Caption { get; private set; }

        public UpdatePieCaptionCommand(Guid id, string caption)
        {
            Verify.NotWhitespace(caption, "caption");            
            Caption = caption;
            Id = id;
        }
    }
}
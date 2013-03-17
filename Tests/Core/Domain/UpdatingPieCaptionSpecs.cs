using Codell.Pies.Core.Domain;
using Codell.Pies.Core.Events;
using Codell.Pies.Testing.BDD;

namespace Codell.Pies.Tests.Core.Domain.UpdatingPieCaptionSpecs
{
    [Concern(typeof (Pie))]
    public class When_updating_a_pie_caption : PieSpecBase
    {
        private string _expected;

        protected override void Given()
        {
            _expected = "My blueberry pie is made up of";
        }

        protected override void When()
        {
            Sut.UpdateCaption(_expected);
        }

        [Observation]
        public void Then_should_announce_that_the_pie_caption_was_updated()
        {
            Verify<PieCaptionUpdatedEvent>(e => e.Caption == _expected);
        }
    }

    [Concern(typeof(Pie))]
    public class When_updating_a_pie_caption_that_has_not_changed : PieSpecBase
    {
        private string _sameCaption;

        protected override void Given()
        {
            _sameCaption = "My blueberry pie is made up of";
           Sut.UpdateCaption(_sameCaption);
        }

        protected override void When()
        {
            Sut.UpdateCaption(_sameCaption);
        }

        [Observation]
        public void Then_should_not_announce_that_the_pie_caption_was_updated()
        {
            Verify<PieCaptionUpdatedEvent>().WasNotPublished();
        }
    }
}
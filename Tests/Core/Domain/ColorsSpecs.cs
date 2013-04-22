using Codell.Pies.Core.Domain;
using Codell.Pies.Testing.BDD;
using FluentAssertions;

namespace Codell.Pies.Tests.Core.Domain.ColorsSpecs
{
    [Concern(typeof (Colors))]
    public class When_getting_the_next_color_and_all_predefined_colors_have_been_used : ContextBase<Colors>
    {
        private string _nextColor;

        protected override void Given()
        {
            foreach (var color in Sut)
            {
                Sut.Next();
            }
        }

        protected override void When()
        {
            _nextColor = Sut.Next();
        }

        [Observation]
        public void Then_should_recycle_colors_and_start_back_at_the_first_color()
        {
            _nextColor.Should().Be(Sut.First());
        }
    }
}
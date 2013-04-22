using Codell.Pies.Core.Domain;
using Codell.Pies.Testing.BDD;
using FluentAssertions;

namespace Codell.Pies.Tests.Core.Domain.TagSpecs
{
    [Concern(typeof (Tag))]
    public class When_creating_a_tag : ContextBase
    {
        private string _tag;

        protected override void When()
        {
            _tag = new Tag(" ABC ");
        }

        [Observation]
        public void Then_should_ensure_the_value_is_lower_case_and_any_padding_is_removed()
        {
            _tag.Should().Be("abc");
        }
    }
}
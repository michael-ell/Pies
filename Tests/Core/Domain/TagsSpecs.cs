using System.Collections.Generic;
using System.Linq;
using Codell.Pies.Core.Domain;
using Codell.Pies.Testing.BDD;
using FluentAssertions;

namespace Codell.Pies.Tests.Core.Domain.TagsSpecs
{
    [Concern(typeof (Tags))]
    public class When_tags_are_empty : ContextBase
    {
        private Tags _tags;

        protected override void When()
        {
            _tags = new Tags(new List<string> {null, "", " "});
        }

        [Observation]
        public void Then_they_should_not_be_included_as_valid_tags()
        {
            _tags.Count().Should().Be(0);
        }
    }

    [Concern(typeof(Tags))]
    public class When_tags_are_less_than_3_characters: ContextBase
    {
        private Tags _tags;

        protected override void When()
        {
            _tags = new Tags(new List<string> { "x", "xy", "  x", "y   " });
        }

        [Observation]
        public void Then_they_should_not_be_included_as_valid_tags()
        {
            _tags.Count().Should().Be(0);
        }
    }

    [Concern(typeof(Tags))]
    public class When_tags_are_greater_than_or_equal_to_3_characters : ContextBase
    {
        private Tags _tags;

        protected override void When()
        {
            _tags = new Tags(new List<string> { "abcd", "efg"} );
        }

        [Observation]
        public void Then_they_should_be_included_as_valid_tags()
        {
            _tags.Count().Should().Be(2);
        }
    }

    [Concern(typeof(Tags))]
    public class When_tags_are_greater_than_or_equal_to_3_characters_but_have_duplicates : ContextBase
    {
        private Tags _tags;

        protected override void When()
        {
            _tags = new Tags(new List<string> { "abcd", "abcd" });
        }

        [Observation]
        public void Then_they_should_only_include_unique_tags()
        {
            _tags.Count().Should().Be(1);
        }
    }

    [Concern(typeof(Tags))]
    public class When_tags_that_are_greater_than_or_equal_to_3_characters_have_preceding_or_trailing_spaces : ContextBase
    {
        private Tags _tags;
        private List<string> _expectedTags;

        protected override void Given()
        {
            _expectedTags = new List<string>{ "abcd", "efg"};
        }

        protected override void When()
        {
            _tags = new Tags(new List<string> { "  abcd", "efg  " });
        }

        [Observation]
        public void Then_the_tags_should_be_trimmed()
        {
            foreach (var expectedTag in _expectedTags)
            {
                _tags.Should().Contain(expectedTag);
            }
        }
    }

    [Concern(typeof(Tags))]
    public class When_tags_that_are_greater_than_or_equal_to_3_characters_have_mixed_case : ContextBase
    {
        private Tags _tags;
        private List<string> _expectedTags;

        protected override void Given()
        {
            _expectedTags = new List<string> { "abcd", "efg" };
        }

        protected override void When()
        {
            _tags = new Tags(new List<string> { "ABCD", "eFg" });
        }

        [Observation]
        public void Then_the_tags_should_be_converted_to_lowercase()
        {
            foreach (var expectedTag in _expectedTags)
            {
                _tags.Should().Contain(expectedTag);
            }
        }
    }


}
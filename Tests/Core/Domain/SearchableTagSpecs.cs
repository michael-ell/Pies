using Codell.Pies.Core.Domain;
using Codell.Pies.Testing.BDD;
using FluentAssertions;

namespace Codell.Pies.Tests.Core.Domain.SearchableTagSpecs
{
    [Concern(typeof(SearchableTag))]
    public class When_creating_a_searchable_tag: ContextBase
    {
        private SearchableTag _searchable;
        private string _expectedTag;
        private string _tag;

        protected override void Given()
        {
            _tag = "TAG";
            _expectedTag = "tag";
        }


        protected override void When()
        {
            _searchable = new SearchableTag(_tag);
        }

        [Observation]
        public void Then_all_search_tags_should_be_in_lowercase()
        {
            _searchable.Value.Should().Be(_expectedTag);
        }
    }

    [Concern(typeof(SearchableTag))]
    public class When_a_tag_is_padded_with_spaces : ContextBase
    {
        private SearchableTag _searchable;
        private string _expectedTag;
        private string _tag;

        protected override void Given()
        {
            _tag = "  tag ";
            _expectedTag = "tag";
        }


        protected override void When()
        {
            _searchable = new SearchableTag(_tag);
        }

        [Observation]
        public void Then_should_trim_the_tag()
        {
            _searchable.Value.Should().Be(_expectedTag);
        }
    }

    [Concern(typeof (SearchableTag))]
    public class When_a_tag_has_a_trailing_comma : ContextBase
    {
        private SearchableTag _searchable;
        private string _expectedTag;
        private string _tag;

        protected override void Given()
        {
            _tag = "tag,";
            _expectedTag = "tag";
        }


        protected override void When()
        {
            _searchable = new SearchableTag(_tag);
        }

        [Observation]
        public void Then_should_assume_it_is_a_spearator_and_remove_it()
        {
            _searchable.Value.Should().Be(_expectedTag);
        }
    }

    [Concern(typeof(SearchableTag))]
    public class When_a_tag_has_a_trailing_semi_colon : ContextBase
    {
        private SearchableTag _searchable;
        private string _expectedTag;
        private string _tag;

        protected override void Given()
        {
            _tag = "tag;";
            _expectedTag = "tag";
        }


        protected override void When()
        {
            _searchable = new SearchableTag(_tag);
        }

        [Observation]
        public void Then_should_assume_it_is_a_spearator_and_remove_it()
        {
            _searchable.Value.Should().Be(_expectedTag);
        }
    }
}
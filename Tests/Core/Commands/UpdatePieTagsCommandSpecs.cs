using System;
using System.Collections.Generic;
using Codell.Pies.Core.Commands;
using Codell.Pies.Testing.BDD;
using FluentAssertions;

namespace Codell.Pies.Tests.Core.Commands.UpdatePieTagsCommandSpecs
{
    [Concern(typeof (UpdatePieTagsCommand))]
    public class When_tags_are_represented_in_one_string_separated_by_spaces : ContextBase
    {
        private Guid _expectedPieId;
        private UpdatePieTagsCommand _command;
        private string _tags;
        private List<string> _expectedTags;

        protected override void Given()
        {
            _expectedPieId = Guid.NewGuid();
            _tags = "x y z";
            _expectedTags = new List<string> {"x", "y", "z"};
        }

        protected override void When()
        {
            _command = new UpdatePieTagsCommand(_expectedPieId, _tags);
        }

        [Observation]
        public void Then_should_have_the_pie_id_to_update()
        {
            _command.Id.Should().Be(_expectedPieId);
        }

        [Observation]
        public void Then_should_split_the_tags_by_spaces()
        {
            foreach (var expectedTag in _expectedTags)
            {
                _command.NewTags.Should().Contain(expectedTag);
            }
        }
    }

    [Concern(typeof(UpdatePieTagsCommand))]
    public class When_tags_are_represented_in_one_string_separated_by_commas : ContextBase
    {
        private Guid _expectedPieId;
        private UpdatePieTagsCommand _command;
        private string _tags;
        private List<string> _expectedTags;

        protected override void Given()
        {
            _expectedPieId = Guid.NewGuid();
            _tags = "x,y,z";
            _expectedTags = new List<string> { "x", "y", "z" };
        }

        protected override void When()
        {
            _command = new UpdatePieTagsCommand(_expectedPieId, _tags);
        }


        [Observation]
        public void Then_should_split_the_tags_by_the_commas()
        {
            foreach (var expectedTag in _expectedTags)
            {
                _command.NewTags.Should().Contain(expectedTag);
            }
        }
    }

    [Concern(typeof(UpdatePieTagsCommand))]
    public class When_tags_are_represented_in_one_string_separated_by_semi_colon : ContextBase
    {
        private Guid _expectedPieId;
        private UpdatePieTagsCommand _command;
        private string _tags;
        private List<string> _expectedTags;

        protected override void Given()
        {
            _expectedPieId = Guid.NewGuid();
            _tags = "x;y;z";
            _expectedTags = new List<string> { "x", "y", "z" };
        }

        protected override void When()
        {
            _command = new UpdatePieTagsCommand(_expectedPieId, _tags);
        }


        [Observation]
        public void Then_should_split_the_tags_by_the_commas()
        {
            foreach (var expectedTag in _expectedTags)
            {
                _command.NewTags.Should().Contain(expectedTag);
            }
        }
    }

    [Concern(typeof(UpdatePieTagsCommand))]
    public class When_tags_are_represented_in_one_string_with_an_unrecognized_separator : ContextBase
    {
        private Guid _expectedPieId;
        private UpdatePieTagsCommand _command;
        private string _tags;
        private List<string> _expectedTags;

        protected override void Given()
        {
            _expectedPieId = Guid.NewGuid();
            _tags = "x~y~z";
            _expectedTags = new List<string> { _tags };
        }

        protected override void When()
        {
            _command = new UpdatePieTagsCommand(_expectedPieId, _tags);
        }


        [Observation]
        public void Then_should_return_the_entered_string()
        {
            foreach (var expectedTag in _expectedTags)
            {
                _command.NewTags.Should().Contain(expectedTag);
            }
        }
    }
}
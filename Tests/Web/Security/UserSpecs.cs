using Codell.Pies.Testing.BDD;
using Codell.Pies.Web.Security;
using FluentAssertions;

namespace Codell.Pies.Tests.Web.Security.UserSpecs
{
    [Concern(typeof (User))]
    public class When_representing_a_user_as_a_string : ContextBase<User>
    {
        private string _asString;

        protected override User CreateSut()
        {
            return new User("x", "y");
        }

        protected override void When()
        {
            _asString = Sut.ToString();
        }

        [Observation]
        public void Then_should_be_id_and_name_delimited_by_a_semi_colon()
        {
            _asString.Should().Be(Sut.Id + ";" + Sut.Name);
        }
    }

    [Concern(typeof(User))]
    public class When_creating_a_user_from_a_proper_string_representation : ContextBase
    {
        private string _data;
        private string _expectedId;
        private string _expectedName;
        private User _user;

        protected override void Given()
        {
            _expectedId = "x";
            _expectedName = "y";
            _data = new User(_expectedId, _expectedName).ToString();
        }

        protected override void When()
        {
            _user = new User(_data);
        }

        [Observation]
        public void Then_should_be_able_to_parse_the_user_id()
        {
            _user.Id.Should().Be(_expectedId);
        }

        [Observation]
        public void Then_should_be_able_to_parse_the_user_name()
        {
            _user.Name.Should().Be(_expectedName);
        }
    }
}
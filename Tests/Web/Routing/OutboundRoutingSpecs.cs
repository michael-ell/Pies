using System;
using Codell.Pies.Testing.BDD;
using Codell.Pies.Web.Routing;

namespace Codell.Pies.Tests.Web.Routing
{
    [Concern(typeof(RouteBuilder))]
    public class When_generating_an_url_for_finding_a_pie_by_tags : ContextBase
    {
        protected override void When()
        {
        }

        [Observation]
        public void Then_should_route_to_the_home_controller_and_find_action()
        {
            Verify.RouteData(new { controller = "home", action = "find" }).Generates("/home/find");
        }
    }

    [Concern(typeof(RouteBuilder))]
    public class When_generating_an_url_for_editing_a_pie : ContextBase
    {
        protected override void When()
        {
        }

        [Observation]
        public void Then_should_provide_a_clean_url_to_edit_a_pie()
        {
            var expectedId = Guid.NewGuid();
            Verify.RouteData(new { controller = "mypies", action = "edit", id = expectedId }).Generates(string.Format("/mypies/edit/{0}", expectedId));
        }
    }

    [Concern(typeof(RouteBuilder))]
    public class When_generating_an_url_for_deleting_a_pie : ContextBase
    {
        protected override void When()
        {
        }

        [Observation]
        public void Then_should_provide_a_clean_url_to_delete_a_pie()
        {
            var expectedId = Guid.NewGuid();
            Verify.RouteData(new { controller = "mypies", action = "delete", id = expectedId }).Generates(string.Format("/mypies/delete/{0}", expectedId));
        }
    }
}
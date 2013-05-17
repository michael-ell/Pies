using Codell.Pies.Testing.BDD;
using Codell.Pies.Web.Routing;

namespace Codell.Pies.Tests.Web.Routing
{
    [Concern(typeof(RouteBuilder))]
    public class When_finding_pies_by_tags: ContextBase
    {
        protected override void When()
        {
        }

        [Observation]
        public void Then_should_route_to_searching_for_pies_by_tag()
        {
            const string expected = "xxx";
            Verify.Url("~/search/find/" + expected).MapsTo(new { controller = "Search", action = "Find", tag = expected });
        }
    }

    [Concern(typeof(RouteBuilder))]
    public class When_asking_to_edit_a_pie : ContextBase
    {
        protected override void When()
        {
        }

        [Observation]
        public void Then_should_route_to_edit_the_pie()
        {
            const string expected = "xxx";
            Verify.Url("~/mypie/edit/" + expected).MapsTo(new { controller = "MyPie", action = "Edit", id = expected });
        }
    }

    [Concern(typeof(RouteBuilder))]
    public class When_asking_to_delete_a_pie : ContextBase
    {
        protected override void When()
        {
        }

        [Observation]
        public void Then_should_route_to_delete_the_pie()
        {
            const string expected = "xxx";
            Verify.Url("~/mypie/delete/" + expected).MapsTo(new { controller = "MyPie", action = "Delete", id = expected });
        }
    }
}
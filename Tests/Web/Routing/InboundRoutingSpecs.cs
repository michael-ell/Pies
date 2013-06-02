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
            Verify.Url("~/search/find/" + expected).MapsTo(new { controller = "Search", action = "Get", tag = expected });
        }
    }

    public class When_asking_for_a_page_of_pies : ContextBase
    {
        protected override void When()
        {
        }

        [Observation]
        public void Then_should_route_to_getting_the_page_for_pies()
        {
            const string expected = "xxx";
            Verify.Url("~/home/page/" + expected).MapsTo(new { controller = "Home", action = "Index", page = expected });
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
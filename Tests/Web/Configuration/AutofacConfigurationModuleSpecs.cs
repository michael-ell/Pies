using Autofac;
using Codell.Pies.Testing.BDD;
using Codell.Pies.Testing.Helpers;
using Codell.Pies.Web.Configuration;

namespace Codell.Pies.Tests.Web.Configuration.AutofacConfigurationModuleSpecs
{
    [Concern(typeof (AutofacConfigurationModule))]
    public class When_registering_implementations : ContextBase<AutofacConfigurationModule>
    {
        private IContainer _container;

        protected override void When()
        {
            _container = AutofacContainer.Setup(Sut);
        }

        [Observation]
        public void Then_should_be_able_to_create_all_registered_implementations()
        {
            _container.Verify();
        }
    }
}
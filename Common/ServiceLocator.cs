namespace Codell.Pies.Common
{
    //Please only use when we outside of the ioc creation pipeline
    public static class ServiceLocator
    {
        private static IServiceLocator _instance;

        public static void RegisterInstance(IServiceLocator locator)
        {
            Verify.NotNull(locator, "obj");
            _instance = locator;
        }

        public static IServiceLocator Instance
        {
            get { return _instance ?? new NullServiceLocator(); }
        }
    }
}
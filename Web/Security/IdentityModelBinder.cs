using System.Web.Mvc;
using Codell.Pies.Web.Extensions;

namespace Codell.Pies.Web.Security
{
    public class IdentityModelBinder : DefaultModelBinder
    {
        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            return bindingContext.ModelType == typeof(IPiesIdentity) ? controllerContext.HttpContext.GetIdentity() 
                                                                     : base.BindModel(controllerContext, bindingContext);
        }
    }
}
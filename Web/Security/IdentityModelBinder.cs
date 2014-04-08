using System.Web;
using System.Web.Http.Controllers;
using System.Web.Mvc;

namespace Codell.Pies.Web.Security
{
    public class IdentityModelBinder : DefaultModelBinder, System.Web.Http.ModelBinding.IModelBinder
    {
        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            return bindingContext.ModelType == typeof(IPiesIdentity) ? controllerContext.HttpContext.GetIdentity() 
                                                                     : base.BindModel(controllerContext, bindingContext);
        }

        public bool BindModel(HttpActionContext actionContext, System.Web.Http.ModelBinding.ModelBindingContext bindingContext)
        {
            if (bindingContext.ModelType != typeof(IPiesIdentity)) return false;
            bindingContext.Model = HttpContext.Current.GetIdentity();
            return true;
        }
    }
}
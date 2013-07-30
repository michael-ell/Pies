using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.WebPages;
using Codell.Pies.Common;
using Codell.Pies.Common.Configuration;
using Codell.Pies.Common.Extensions;
using Codell.Pies.Core.Domain;

namespace Codell.Pies.Web.Extensions
{
    public static class HtmlHelperExtensions
    {
        public static MvcHtmlString EnumDropDownListFor<TModel, TEnum>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TEnum>> expression)
        {
            return htmlHelper.EnumDropDownListFor(expression, null);
        }

        public static MvcHtmlString EnumDropDownListFor<TModel, TEnum>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TEnum>> expression, IDictionary<string, object> htmlAttributes)
        {
            return htmlHelper.EnumDropDownListFor(expression, (object)htmlAttributes);
        }

        public static MvcHtmlString EnumDropDownListFor<TModel, TEnum>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TEnum>> expression, object htmlAttributes)
        {
            var metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            var enumType = GetNonNullableModelType(metadata);
            var values = Enum.GetValues(enumType).Cast<TEnum>();

            var items = values.Select(value => new SelectListItem
                                                   {
                                                       Text = value.ToDescription(),
                                                       Value = value.ToString(),
                                                       Selected = value.Equals(metadata.Model)
                                                   });

            if (metadata.IsNullableValueType)
            {
                items = SingleEmptyItem.Concat(items);
            }

            return htmlHelper.DropDownListFor(expression, items, htmlAttributes);
        }

        private static Type GetNonNullableModelType(ModelMetadata modelMetadata)
        {
            Type realModelType = modelMetadata.ModelType;

            Type underlyingType = Nullable.GetUnderlyingType(realModelType);
            if (underlyingType != null)
            {
                realModelType = underlyingType;
            }
            return realModelType;
        }

        private static readonly SelectListItem[] SingleEmptyItem = new[] { new SelectListItem { Text = Resources.SelectPrompt, Value = "" } };

        public static string Version(this HtmlHelper htmlHelper)
        {
            return InformationAssembly().GetName().Version.ToString();
        }

        public static string ProductName(this HtmlHelper htmlHelper)
        {
            var productInfo =  InformationAssembly().GetCustomAttributes(typeof (AssemblyProductAttribute), false).FirstOrDefault();
            if (productInfo == null) return Resources.Unknown;
            var name = ((AssemblyProductAttribute) productInfo).Product;
            return StringExtensions.IsEmpty(name) ? Resources.Unknown : name;
        }

        public static string EncodedProductName(this HtmlHelper htmlHelper)
        {
            return HttpUtility.UrlPathEncode(htmlHelper.ProductName());
        }

        public static string ServerName(this HtmlHelper htmlHelper)
        {
            return Environment.MachineName;
        }

        private static Assembly InformationAssembly()
        {
            return Assembly.GetAssembly(typeof(Pie));
        }

        public static string GetAppSetting(this HtmlHelper htmlHelper, string key)
        {
            var settings = new AppSettings();
            return settings.Get<string>(key);            
        }

        public static MvcHtmlString TextBoxFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, IgnoreHtmlFieldPrefix ignoreHtmlFieldPrefix)
        {
            if (ignoreHtmlFieldPrefix)
            {
                var pi = (PropertyInfo) ((MemberExpression) expression.Body).Member;
                return htmlHelper.TextBoxFor(expression, new {pi.Name, Id = pi.Name});
            }
            return htmlHelper.TextBoxFor(expression);
        }

        public static MvcHtmlString HiddenFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, IgnoreHtmlFieldPrefix ignoreHtmlFieldPrefix)
        {
            if (ignoreHtmlFieldPrefix)
            {
                var pi = (PropertyInfo)((MemberExpression)expression.Body).Member;
                return htmlHelper.HiddenFor(expression, new { pi.Name, Id = pi.Name });
            }
            return htmlHelper.HiddenFor(expression);
        }

        public static string CheckOutMyPie(this HtmlHelper helper, string url)
        {
            var subject = EncodeQueryString("Check out my Pie");
            var body = string.Format("{0}%0D%0A%0D%0A{1}", EncodeQueryString("Click the link below to check out my pie!"), EncodeQueryString(url));
            return string.Format("mailto:?Subject={0}&Body={1}", subject, body);
        }

        public static string LetsMakeAPie(this HtmlHelper helper, string url)
        {
            var subject = EncodeQueryString("Let's make a Pie");
            var body = string.Format("{0}%0D%0A%0D%0A{1}", EncodeQueryString("Click the link below to bake a pie with me!"), EncodeQueryString(url));
            return string.Format("mailto:?Subject={0}&Body={1}", subject, body);
        }

        private static string EncodeQueryString(string queryString)
        {
            return HttpUtility.UrlPathEncode(queryString.Replace("%", "%25"));
        }

        public static MvcHtmlString Tab(this HtmlHelper helper, string text, string action, string controller, string altAction = "", string altController = "")
        {
            var routeData = helper.ViewContext.RouteData.Values;
            var currentController = routeData["controller"];
            var currentAction = (string)routeData["action"];

            if ((String.Equals(action, currentAction, StringComparison.OrdinalIgnoreCase) &&
                 String.Equals(controller, currentController as string, StringComparison.OrdinalIgnoreCase)) ||
                (String.Equals(altAction, currentAction, StringComparison.OrdinalIgnoreCase) &&
                 String.Equals(altController, currentController as string, StringComparison.OrdinalIgnoreCase))                
                )
            {
                return new MvcHtmlString(string.Format("<li class='selected'>{0}</li>", helper.ActionLink(text, action, controller, null, new { @class = "currentMenuItem" })));
            }
            return new MvcHtmlString(string.Format("<li>{0}</li>", helper.ActionLink(text, action, controller, null, new { @class = "currentMenuItem" })));
        }

        public static MvcHtmlString Tab(this HtmlHelper helper, string text, TabAction primary,  params TabAction[] alternates)
        {
            var routeData = helper.ViewContext.RouteData.Values;
            var current = new TabAction((string) routeData["action"], (string)routeData["controller"]);
            alternates = alternates ?? new TabAction[0];

            if (primary.Equals(current) || alternates.Any(action => action.Equals(current)))
            {
                return new MvcHtmlString(string.Format("<li class='selected'>{0}</li>", helper.ActionLink(text, primary.Action, primary.Controller, null, new { @class = "currentMenuItem" })));
            }
            return new MvcHtmlString(string.Format("<li>{0}</li>", helper.ActionLink(text, primary.Action, primary.Controller, null, new { @class = "currentMenuItem" })));
        }
    }

    public class TabAction
    {
        public TabAction(string action, string controller)
        {
            Verify.NotWhitespace(action, "action");
            Verify.NotWhitespace(controller, "controller");

            Action = action;
            Controller = controller;
        }

        public string Action { get; private set; }
        public string Controller { get; private set; }

        protected bool Equals(TabAction other)
        {
            return string.Equals(Action, other.Action) && string.Equals(Controller, other.Controller);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((TabAction) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((Action != null ? Action.GetHashCode() : 0)*397) ^ (Controller != null ? Controller.GetHashCode() : 0);
            }
        }
    }
}
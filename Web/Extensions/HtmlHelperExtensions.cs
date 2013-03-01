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

        public static MvcHtmlString SpanFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, object htmlAttributes)
        {
            var text = expression.Compile()(htmlHelper.ViewData.Model).SafeToString();
            if (string.IsNullOrEmpty(text))
                return MvcHtmlString.Create("&nbsp");
            
            var span = new TagBuilder("span");
            span.MergeAttributes(HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
            span.SetInnerText(text);
            return MvcHtmlString.Create(span.ToString());
        } 

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
    }
}
﻿using System;
using System.Web;
using System.Web.Security;
using Codell.Pies.Common;
using DotNetOpenAuth.AspNet;

namespace Codell.Pies.Web.Security
{
    public static class HttpContextExtensions
    {
        public static void SetUser(this HttpContextBase context, AuthenticationResult result)
        {
            context.SetUser(new User(result.ProviderUserId, result.UserName));          
        }

        public static void SetUser(this HttpContextBase context, User user)
        {
            var ticket = new FormsAuthenticationTicket(1, user.Name, DateTime.Now, DateTime.Now.AddDays(7), true, user.ToString());
            var encrypted = FormsAuthentication.Encrypt(ticket);
            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encrypted);
            context.Response.Cookies.Add(cookie); 
        }

        public static void ClearUser(this HttpContextBase context)
        {
            FormsAuthentication.SignOut();
        }

        public static IPiesIdentity GetIdentity(this HttpContext context)
        {
            return GetIdentity(new HttpContextWrapper(context));
        }

        public static IPiesIdentity GetIdentity(this HttpContextBase context)
        {
            if (context == null || context.Request == null) return new AnonymousIdentity();   

            var cookie = context.Request.Cookies[FormsAuthentication.FormsCookieName];
            if (cookie == null) return new AnonymousIdentity();

            var ticket = cookie.Value;
            if (ticket.IsEmpty()) return new AnonymousIdentity();

            var decryptedTicket = FormsAuthentication.Decrypt(ticket);
            if (decryptedTicket == null) return new AnonymousIdentity();

            return new Identity(new User(decryptedTicket.UserData));            
        }
    }
}
using System;
using Codell.Pies.Common.Security;
using DotNetOpenAuth.OpenId.Extensions.SimpleRegistration;

namespace Codell.Pies.Web.Security
{
    public class OpenIdUser : IUser
    {
        public static OpenIdUser Anonymous = new OpenIdUser();

        public string Email { get; private set; }

        public string Nickname { get; private set; }

        public string FullName { get; private set; }

        public bool IsSignedByProvider { get; private set; }

        public string ClaimedIdentifier { get; private set; }

        private OpenIdUser()
        {
            Email = string.Empty;
            Nickname = string.Empty;
            FullName = string.Empty;
            IsSignedByProvider = false;
            ClaimedIdentifier = string.Empty;
        }

        public OpenIdUser(string data)
        {
            PopulateFromDelimitedString(data);
        }

        public OpenIdUser(ClaimsResponse claim, string identifier)
        {
            AddClaimInfo(claim, identifier);
        }

        private void AddClaimInfo(ClaimsResponse claim, string identifier)
        {
            Email = claim.Email.ToLower();
            FullName = claim.FullName;
            Nickname = claim.Nickname ?? claim.Email;
            IsSignedByProvider = claim.IsSignedByProvider;
            ClaimedIdentifier = identifier;
        }

        private void PopulateFromDelimitedString(string data)
        {
            if (data.Contains(";"))
            {
                var stringParts = data.Split(';');
                if (stringParts.Length > 0) Email = stringParts[0];
                if (stringParts.Length > 1) FullName = stringParts[1];
                if (stringParts.Length > 2) Nickname = stringParts[2];
                if (stringParts.Length > 3) ClaimedIdentifier = stringParts[3];
            }
        }

        public override string ToString()
        {
            return String.Format("{0};{1};{2};{3}", Email, FullName, Nickname, ClaimedIdentifier);
        }
    }
}
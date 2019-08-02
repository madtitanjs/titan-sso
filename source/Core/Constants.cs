using System;
using System.Collections.Generic;

namespace SSO.Core
{
    public class Constants
    {
        public static readonly TimeSpan DefaultTokenExpiration = TimeSpan.FromHours(10);
        public static class ProviderCookies
        {
            public static readonly string Facebook = "FACEBOOK_COOKIE_AUTH";
            public static readonly string Google = "GGL_COOKIE_AUTH";
            public static readonly string Twitter = "TWITR_COOKIE_AUTH";
        }

        public static class Errors
        {
            public static readonly string ConcurrencyError = "concurreny error";
            public static readonly string NotFound = "not found";
            public static readonly string CantInsert = "cant insert";

            public static readonly string InvalidGrantType =
                "Invalid Grant Type. " +
                $"Allowed GrantTypes are {string.Join(",", Client.GrantTypes)}";

            public static readonly string UserNotFound = "User does not exist";
            public static readonly string InvalidCredentials = "Invalid user credentials";
        }
        public static class Client
        {
            public static readonly List<string> SecretTypes = new List<string>
            {
                "SharedSecret",
                "X509Thumbprint",
                "X509Name",
                "X509CertificateBase64"
            };

            public static readonly List<string> GrantTypes = new List<string>
            {
                "implicit",
                "client_credentials",
                "authorization_code",
                "hybrid",
                "password",
                "urn:ietf:params:oauth:grant-type:device_code"
            };


            public static readonly List<string> ProtocolTypes = new List<string>
            {
                ProtocolType.OIDC
            };

            public static class ProtocolType
            {
                public static readonly string OIDC = "oidc";
            }
        }
    }
}

﻿using System;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;

namespace giftideas.Auth
{
    public class RapidApiAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        public RapidApiAuthenticationHandler(
            IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock)
            : base(options, logger, encoder, clock) { }

        private const string RapidApiSecretHeaderName = "X-Mashape-Proxy-Secret";
        private const string RapidApiCorrectSecret = "1234";

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            string providedSecret = Request.Headers[RapidApiSecretHeaderName];
            if (string.IsNullOrEmpty(providedSecret))
            {
                return AuthenticateResult.NoResult();
            }

            if (string.IsNullOrEmpty(providedSecret) || !providedSecret.Equals(RapidApiCorrectSecret))
            {
                return AuthenticateResult.NoResult();
            }

            // todo: replace with rapidapi username header
            string userId = "RapidApi";

            var claims = new[] { new Claim(ClaimTypes.Name, userId, ClaimValueTypes.String, ClaimsIssuer) };
            var principal = new ClaimsPrincipal(new ClaimsIdentity(claims, Scheme.Name));
            var ticket = new AuthenticationTicket(principal, null, Scheme.Name);
            return AuthenticateResult.Success(ticket);
        }
    }
}
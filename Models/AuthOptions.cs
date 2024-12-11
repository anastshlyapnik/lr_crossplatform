using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Shlyapnikova_lr.Models
{

    internal class AuthOptions
    {
        public static string Issuer => "SAA";
        public static string Audience => "APIclients";
        public static int LifetimeInYears => 1;
        public static SecurityKey SigningKey => new SymmetricSecurityKey(Encoding.ASCII.GetBytes("superSecretKeyMustBeLooooooooong"));

        internal static object GenerateToken(bool is_admin = false)
        {
            var now = DateTime.UtcNow;
            var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, "user"),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, is_admin?"admin":"guest")
                };
            ClaimsIdentity identity =
            new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);
            // создаем JWT-токен
            var jwt = new JwtSecurityToken(
                    issuer: Issuer,
                    audience: Audience,
                    notBefore: now,
                    expires: now.AddYears(LifetimeInYears),
                    claims: identity.Claims,
                    signingCredentials: new SigningCredentials(SigningKey, SecurityAlgorithms.HmacSha256)); ;
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
            return new { token = encodedJwt };

        }
    }
}

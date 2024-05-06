using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

namespace WebAPI.Model
{
    public class JWTTokenHandler
    {
        public string iss { get; set; }
        public string aud { get; set; }
        public ClaimsIdentity sub { get; set; }
        public int exp { get; set; }
        public string nbf { get; set; }
        public string iat { get; set; }
        public string jti { get; set; }
        public string signkey { get; set; }

        private readonly string _signkey;

        public JWTTokenHandler(string signkey)
        {
            _signkey = signkey;
        }

        public string GetJWTToken()
        {
            // STEP1: 建立使用者的 Claims 聲明，這會是 JWT Payload 的一部分
            ClaimsIdentity userClaims = sub;

            // STEP2: 取得對稱式加密 JWT Signature 的金鑰
            // 這部分是選用，但此範例在 Startup.cs 中有設定 ValidateIssuerSigningKey = true 所以這裡必填
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_signkey));
            // STEP3: 建立 JWT TokenHandler 以及用於描述 JWT 的 TokenDescriptor
            var tokenHandler = new JsonWebTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = iss,
                Audience = aud,
                Subject = userClaims,
                Expires = DateTime.Now.AddMinutes(exp),
                SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256)
            };
            // 產出 JWT
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return token.ToString();
        }
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using WebAPI.Model;

namespace WebAPI.Controllers
{
    [AllowAnonymous]
    [Route("[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration _config;

        public LoginController(IConfiguration configuration)
        {
            _config = configuration;
        }

        // GET api/auth/login
        [HttpPost]
        public IActionResult Login([FromBody] LoginInfo loginInfo )
        {
            // STEP0: 在產生 JWT Token 之前，可以依需求做身分驗證
            
            // STEP1: 建立使用者的 Claims 聲明，這會是 JWT Payload 的一部分
            var userClaims = new ClaimsIdentity(new[] {
            new Claim(JwtRegisteredClaimNames.NameId, loginInfo.userID),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            //new Claim("Custom", "Anything You Like")
            });
            //取回JWTToken字串
            JWTTokenHandler JWTTokenHandler = new JWTTokenHandler(_config["JWTConfig:SignKey"]);
            //傳入ID及JWTToken作為userInfo的建構子
            UserInfo userInfo = new UserInfo(loginInfo.userID, JWTTokenHandler.GetJWTToken());
            //取得其他使用者相關資訊
            userInfo.GetUserInfo(userInfo);
                        
            return Ok(JsonSerializer.Serialize(userInfo));
        }
    }
}

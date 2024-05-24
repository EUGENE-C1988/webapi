using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Text.Json;
using WebAPI.Model;

namespace WebAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IConfiguration _admin;

        public MenuController(IConfiguration configuration, IConfiguration admin)
        {
            _configuration = configuration;
            _admin = admin;
        }

        
        [HttpGet]
        public IActionResult GetMenu(string UserID)
        {
            List<Menu> list = new List<Menu>();
            Menu menu = new Menu();
            if (UserID == _configuration["AdministratorAccount:Account"])
            {
                list= menu.GetMenusAdmin();
            }
            else
            {
                list = menu.GetMenuID(UserID);
            }
            //判斷是否為admin
            return Ok(list);


        }
    }
}

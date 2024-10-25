using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Model;

namespace WebAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SysAuthController : ControllerBase
    {
        //Get one element
        private readonly Roles _RolesRepository;
        private readonly Accounts _AccountsRepository;

        public SysAuthController()
        {
            this._RolesRepository = new Roles();
            this._AccountsRepository = new Accounts();
        }

        /// <summary>
        /// 取得所有資料
        /// </summary>
        /// <returns></returns>
        //Get All Data
        [HttpGet("R")]
        public List<Roles> GetRoleList()

        {
            return this._RolesRepository.GetList();
        }

        [HttpGet("A")]
        public List<Accounts> GetAccountList()
        {
            return this._AccountsRepository.GetAllList();
        }

    }
}

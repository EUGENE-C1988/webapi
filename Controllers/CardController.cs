using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Model;
using WebAPI.Parameter;

namespace WebAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class CardController: ControllerBase
    {
        private readonly So _soRepository;

        public CardController() {
            this._soRepository = new So();
        }

        /// <summary>
        /// 取得所有資料
        /// </summary>
        /// <returns></returns>
        //Get All Data
        [HttpGet]
        public List<So> GetList()
        {
            return this._soRepository.GetList();
        }

        //Get one element
        [HttpGet("{id}")]
        //[Route("{id}")]
        public List<So> Get([FromRoute] int id)
        {
            return this._soRepository.GetElement(id);
        }

        //ADD
        [HttpPost]
        public IActionResult Insert([FromBody] SoParameter parameter) {
            SoParameter _insertpara = new SoParameter
            {
                OrderNo = parameter.OrderNo,
                OrderItem = parameter.OrderItem,
                ProductName = parameter.ProductName,
                Qty = parameter.Qty,
                Price = parameter.Price,
                Amount = parameter.Amount,
            };
          
            var result = _insertpara.InsertDB(_insertpara);
            return Ok();
        }

        //UPDATE
        [HttpPut]
        [Route("{id}")]
        public  IActionResult Update([FromRoute] int id, [FromBody] SoParameter parameter)
        {
            
            SoParameter _insertpara = new SoParameter
            {
                OrderNo = parameter.OrderNo,
                OrderItem = parameter.OrderItem,
                ProductName = parameter.ProductName,
                Qty = parameter.Qty,
                Price = parameter.Price,
                Amount = parameter.Amount
                
            };
            var result = _insertpara.UpdateDB(_insertpara,id);
            if (result == 0)
            {
                return NotFound();
            }
            return Ok();
        }

        //DELETE
        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            var result = new SoParameter().DeleteDB(id);
            if (result == 0)
            {
                return NotFound();
            }
            return Ok();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EntegyAPI.Interface;

namespace EntegyAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ChequeController : ControllerBase
    {
        private readonly IChequeService _service;

        public ChequeController(IChequeService service)
        {
            _service = service;
        }

        // GET api/v1/values/2.5
        [HttpGet("{amt}")]
        public ActionResult<string> Get(string amt)
        {
            var result = _service.ConvertDecimalToWord(amt);

            return Ok(result);
        }
    }
}

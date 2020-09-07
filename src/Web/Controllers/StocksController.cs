using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StocksController : ControllerBase
    {
        [HttpGet]
        public string GetStocks()
        {
            return "this will return a list of stocks";
        }

        [HttpGet("{id}")]
        public string GetStock(int id)
        {
            return "this will return one stock";
        }
    }
}
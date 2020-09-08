﻿using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StocksController : ControllerBase
    {
        private readonly CatalogContext _context;

        public StocksController(CatalogContext context)
        {
            _context = context;
        }

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
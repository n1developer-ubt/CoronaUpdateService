using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace CoronaUpdateService.Controllers
{
    public class NewDataController:Controller
    {
        [HttpGet("api/v1/NewData")]
        public ActionResult Get()
        {

        }
    }
}

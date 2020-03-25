using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoronaUpdateService.Services;
using Microsoft.AspNetCore.Mvc;

namespace CoronaUpdateService.Controllers
{
    public class NewDataController:Controller
    {
        [HttpGet("api/v1/NewData")]
        public ActionResult Get()
        {
            return Content(UpdateDataService.GetUpdatedData(), "application/json");
        }
    }
}

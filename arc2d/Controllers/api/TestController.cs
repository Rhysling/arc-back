using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace arc2d.Controllers.api
{
  [Route("api/[controller]")]
  [ApiController]
  public class TestController : ControllerBase
  {

    [HttpGet]
    public ActionResult Get()
    {
      return Ok(new { Msg = "Hello from test controller" });
    }

  }
}
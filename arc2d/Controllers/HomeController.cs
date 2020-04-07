using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace arc2d.Controllers
{
  
  //[ApiController]
  public class HomeController : ControllerBase
  {
    //[Route("")]
    //[Route("Home")]
    //[Route("Home/Index")]
    public IActionResult Index()
    {
      return File("~/index.html", "text/html");
    }

  }
}
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ASPCMVC06.Controllers
{
    public class TMResearchController : Controller
    {
        public IActionResult M01(string id)
        {
            return Content("GET:M01 " + id);
        }

        public IActionResult M02(string id)
        {
            return Content("GET:M02 " + id);
        }

        public IActionResult M03(string id)
        {
            return Content("GET:M03 " + id);
        }

        public IActionResult MXX()
        {
            return Content("GET:MXX");
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Globalization;



namespace ASPCMVC03.Controllers
{
    public class SetCultureAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var culture = "en-US";

            CultureInfo.CurrentCulture = new CultureInfo(culture);
            CultureInfo.CurrentUICulture = new CultureInfo(culture);

            base.OnActionExecuting(filterContext);
        }
    }

    [SetCulture]
    public class ParmController : Controller
    {
        public IActionResult Index(string Id)
        {
            ViewBag.Id = Id;    return View();
        }
        public IActionResult Uri01(int Id)
        {
            ViewBag.Id = Id; return View();
        }
        public IActionResult Uri02(int? Id)
        {
            ViewBag.Id = Id; return View();
        }
        public IActionResult Uri03(float Id)
        {
            ViewBag.Id = Id; return View();
        }
        public IActionResult Uri04(DateTime Id)
        {
            ViewBag.Id = Id; return View();
        }
    }
}

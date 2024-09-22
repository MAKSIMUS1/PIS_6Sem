using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace Lab_3a.Controllers
{
    public class CalcController : Controller
    {
        private bool TryParseFloat(string value, out float result)
        {
            return float.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture, out result)
                   && !float.IsNaN(result)
                   && !float.IsInfinity(result)
                   && result >= float.MinValue
                   && result <= float.MaxValue;
        }

        [HttpGet]
        public IActionResult Index(string operation)
        {
            ViewBag.press = operation;
            return View("Calc");
        }

        [HttpPost]
        public IActionResult Sum(string x, string y)
        {
            if (!TryParseFloat(x, out float xValue) || !TryParseFloat(y, out float yValue))
            {
                ViewBag.press = "+";
                ViewBag.Error = "Invalid input: One or both of the numbers are out of the valid range.";
                return View("Calc");
            }

            ViewBag.x = xValue;
            ViewBag.y = yValue;
            ViewBag.z = xValue + yValue;
            ViewBag.press = "+";
            return View("Calc");
        }

        [HttpPost]
        public IActionResult Sub(string x, string y)
        {
            if (!TryParseFloat(x, out float xValue) || !TryParseFloat(y, out float yValue))
            {
                ViewBag.press = "-";
                ViewBag.Error = "Invalid input: One or both of the numbers are out of the valid range.";
                return View("Calc");
            }

            ViewBag.x = xValue;
            ViewBag.y = yValue;
            ViewBag.z = xValue - yValue;
            ViewBag.press = "-";
            return View("Calc");
        }

        [HttpPost]
        public IActionResult Mul(string x, string y)
        {
            if (!TryParseFloat(x, out float xValue) || !TryParseFloat(y, out float yValue))
            {
                ViewBag.press = "*";
                ViewBag.Error = "Invalid input: One or both of the numbers are out of the valid range.";
                return View("Calc");
            }

            ViewBag.x = xValue;
            ViewBag.y = yValue;
            ViewBag.z = xValue * yValue;
            ViewBag.press = "*";
            return View("Calc");
        }

        [HttpPost]
        public IActionResult Div(string x, string y)
        {
            if (!TryParseFloat(x, out float xValue) || !TryParseFloat(y, out float yValue))
            {
                ViewBag.press = "/";
                ViewBag.Error = "Invalid input: One or both of the numbers are out of the valid range.";
                return View("Calc");
            }

            ViewBag.x = xValue;
            ViewBag.y = yValue;
            if (yValue == 0)
            {
                ViewBag.Error = "Division by zero is not allowed.";
                ViewBag.z = null;
            }
            else
            {
                ViewBag.z = xValue / yValue;
            }
            ViewBag.press = "/";
            return View("Calc");
        }
    }
}

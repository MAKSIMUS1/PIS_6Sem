using Microsoft.AspNetCore.Mvc;

namespace ASPCMVC07.Controllers
{
    [Route("it")]
    public class TAResearchController : Controller
    {
        [HttpGet("{n:int}/{str}", Order = -10)]
        public IActionResult M04(int? n, string? str)
        {
            try
            {
                return Ok($"GET:M04:/{n}/{str}");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [AcceptVerbs("POST", "GET"), Route("{b:bool}/{letters::letters}")]
        public IActionResult M05(bool? b, string? letters)
        {
            try
            {
                return Ok($"{HttpContext.Request.Method}:M05: /{b}/{letters}");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [AcceptVerbs("DELETE", "GET"), Route("{f:float}/{str::length(2, 5)}")]
        public IActionResult M06(float? f, string? str)
        {
            try
            {
                return Ok($"{HttpContext.Request.Method}:M06 /{f}/{str}");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{letters::length(3, 4)::alpha}/{n::range(100, 200)}")]
        public IActionResult M07(string? letters, int? n)
        {
            try
            {
                return Ok($"PUT:M07 /{letters}/{n}");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("{mail::email}")]
        public IActionResult M08(string? mail)
        {
            try
            {
                return Ok($"POST:M08 /{mail}");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

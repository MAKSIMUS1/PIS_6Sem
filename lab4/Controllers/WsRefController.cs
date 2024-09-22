using lab3b_vd.Attributes;
using lab3b_vd.DTO.WsRef;
using lab3b_vd.Models;
using lab3b_vd.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace lab3b_vd.Controllers;

[Route("WsRef")]
[Route("")]
[AuthorizeAsGuestIfNotAuthorized]
public class WsRefController : Controller
{
    private readonly WsRefService wsRefService;
    private readonly WsRefCommentsService wsRefCommentsService;

    public WsRefController(WsRefService wsRefService, WsRefCommentsService wsRefCommentsService)
    {
        this.wsRefService = wsRefService;
        this.wsRefCommentsService = wsRefCommentsService;
    }

    [ActionContext]
    public ActionContext actionContext { get; set; }

    public async Task<IActionResult> Index(string filter = null, int? updateId = null, int? comments = null)
    {
        var filterList = filter?.Split(',', ';') ?? [];
        var wsRefs = (await wsRefService.GetWsRefsAsync())
            .OrderByDescending(wsr => wsr.Plus - wsr.Minus)
            .ToList();

        if (filterList.Length > 0)
            wsRefs = wsRefs
                .Where(wsRef => filterList
                    .Any(el => wsRef
                        .Description
                        .ToLower()
                        .Contains(el.ToLower())))
                .ToList();

        ViewBag.UpdatedId = updateId;
        ViewBag.IsWithFilter = filterList.Length > 0;

        if (comments is not null)
        {
            var commentsList = await wsRefCommentsService.GetCommentsOfAsync(comments.Value);

            ViewBag.CommentsInfo = new
            {
                WsRefId = comments,
                Comments = commentsList
            };
        }

        ViewBag.SessionId = actionContext.HttpContext.Session.Id;

        return View("Index", wsRefs);
    }

    #region Insert

    [HttpGet("Insert")]
    [Authorize(Roles = "Owner")]
    public IActionResult InsertForm()
    {
        return View("Insert");
    }

    [HttpPost("Insert")]
    [Authorize(Roles = "Owner")]
    public async Task<IActionResult> InsertAsync(WsRefInsertDto dto)
    {
        try
        {
            if (!ModelState.IsValid)
                throw new Exceptions.ValidationException(ModelState);

            using var httpClient = new HttpClient();
            var resp = await httpClient.GetAsync(dto.Url);

            if (!resp.IsSuccessStatusCode)
                throw new("There are no resources in url " + dto.Url);

            var wsRef = new WsRef()
            {
                Url = dto.Url,
                Description = dto.Description
            };

            await wsRefService.AddWsRefAsync(wsRef);
            return RedirectToAction("Index");
        }
        catch (Exceptions.ValidationException e)
        {
            ViewBag.Errors = e.ValidationErrors;
            return View("Insert");
        }
        catch (Exception e)
        {
            ViewBag.Errors = new List<string>() {e.Message};
            return View("Insert");
        }
    }

    #endregion

    #region Delete

    [HttpPost("Delete")]
    [Authorize(Roles = "Owner")]
    public async Task<IActionResult> DeleteWsRefAsync(int id)
    {
        try
        {
            _ = await wsRefService.DeleteWsRefAsync(id);
            return Redirect("/WsRef");
        }
        catch
        {
            return Redirect("/Admin/Error?message=Delete finished error");
        }
    }

    #endregion

    #region Update

    [HttpPost("Update")]
    [Authorize(Roles = "Owner")]
    public async Task<IActionResult> UpdateWsRefAsync(WsRefUpdateDto dto)
    {
        try
        {
            if (!ModelState.IsValid)
                throw new Exceptions.ValidationException(ModelState);

            using var httpClient = new HttpClient();
            var resp = await httpClient.GetAsync(dto.Url);

            if (!resp.IsSuccessStatusCode)
                throw new("There are no resources in url " + dto.Url);

            var wsRef = await wsRefService.GetWsRefByIdAsync(dto.Id);

            if (wsRef is null)
                throw new("WsRef is not found");

            wsRef.Url = dto.Url;
            wsRef.Description = dto.Description;

            await wsRefService.UpdateWsRefAsync(wsRef);
            return RedirectToAction("Index");
        }
        catch (Exceptions.ValidationException e)
        {
            ViewBag.Errors = e.ValidationErrors;
            return Redirect("/WsRef?updateId=" + dto?.Id);
        }
        catch (Exception e)
        {
            ViewBag.Errors = new List<string>() { e.Message };
            return View("Insert");
        }
    }

    #endregion

    #region Plus and Minus

    [HttpPost("{id:int}/Plus")]
    [Authorize(Roles = "Owner")]
    public async Task<IActionResult> Plus(int id, string? filter, int? updateId)
    {
        var wsRef = await wsRefService.PlusAsync(id);
        return RedirectToAction("Index", new { filter = filter, updateId = updateId });
    }

    [HttpPost("{id:int}/Minus")]
    [Authorize(Roles = "Owner")]
    public async Task<IActionResult> Minus(int id, string? filter, int? updateId)
    {
        var wsRef = await wsRefService.MinusAsync(id);
        return RedirectToAction("Index", new { filter = filter, updateId = updateId });
    }

    #endregion
}

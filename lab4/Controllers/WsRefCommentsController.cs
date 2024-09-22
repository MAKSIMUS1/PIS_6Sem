using lab3b_vd.Attributes;
using lab3b_vd.DTO.WsRefComments;
using lab3b_vd.Exceptions;
using lab3b_vd.Models;
using lab3b_vd.Services;
using Microsoft.AspNetCore.Mvc;

namespace lab3b_vd.Controllers;

[Route("WsRefComments")]
[AuthorizeAsGuestIfNotAuthorized]
public class WsRefCommentsController : Controller
{
    private readonly WsRefCommentsService wsRefCommentsService;

    [ActionContext]
    public ActionContext ActionContext { get; set; }

    public WsRefCommentsController(WsRefCommentsService wsRefCommentsService)
    {
        this.wsRefCommentsService = wsRefCommentsService;
    }

    [HttpGet("Add")]
    public IActionResult AddForm(int to)
    {
        return View("Add", to);
    }

    [HttpPost("Add")]
    public async Task<IActionResult> AddAsync(WsRefCommentsAdd dto)
    {
        try
        {
            if (!ModelState.IsValid)
                throw new ValidationException(ModelState);

            var httpContext = ActionContext.HttpContext;
            var sessionId = httpContext.Session.Id;

            var comment = new WsRegComment()
            {
                WsRefId = dto.WsRefId,
                Comment = dto.Comment,
                SessionId = sessionId,
            };

            await wsRefCommentsService.AddCommentAsync(comment);
            return RedirectToAction("Index", "WsRef", new { comments = dto.WsRefId });
        }
        catch (ValidationException e)
        {
            ViewBag.Errors = e.ValidationErrors;
            return View("Add", dto.WsRefId);
        }
        catch (Exception e)
        {
            return Redirect("/Admin/Error?message=" + e.Message);
        }
    }

    [HttpGet("Update")]
    public async Task<IActionResult> UpdateFormAsync(int commentId)
    {
        var sessionId = ActionContext.HttpContext.Session.Id;
        var comment = await wsRefCommentsService.GetCommentByIdAsync(commentId);

        if (comment is null)
            return Redirect("/Admin/Error?message=Comment not found");

        if (!User.IsInRole("Owner") && comment.SessionId != sessionId)
            return Redirect("/Admin/Error?message=Comment is not yours");

        return View("Update", comment);
    }

    [HttpPost("Update")]
    public async Task<IActionResult> UpdateAsync(WsRefUpdateDto dto)
    {
        try
        {
            if (!ModelState.IsValid)
                throw new ValidationException(ModelState);

            var refRegComment = await wsRefCommentsService.GetCommentByIdAsync(dto.Id);
            if (refRegComment is null)
                return Redirect("/Admin/Error?message=Comment not found");

            var sessionId = ActionContext.HttpContext.Session.Id;
            if (!User.IsInRole("Owner") && refRegComment.SessionId != sessionId)
                return Redirect("/Admin/Error?message=Comment is not yours");

            refRegComment.Comment = dto.Comment;

            await wsRefCommentsService.UpdateCommentAsync(refRegComment);
            return RedirectToAction("Index", "WsRef", new { comments = refRegComment.WsRefId });
        }
        catch (ValidationException e)
        {
            ViewBag.Errors = e.ValidationErrors;
        }
        catch (Exception e)
        {
            ViewBag.Errors = new string[] { e.Message };
        }

        return View("Update", new WsRegComment()
        {
            Id = dto.Id,
            Comment = dto.Comment
        });

    }

    [HttpGet("Delete")]
    public async Task<IActionResult> DeleteAsync(int commentId)
    {
        var sessionId = ActionContext.HttpContext.Session.Id;
        var comment = await wsRefCommentsService.GetCommentByIdAsync(commentId);

        if (comment is null)
            return Redirect("/Admin/Error?message=Comment not found");

        if (!User.IsInRole("Owner") && comment.SessionId != sessionId)
            return Redirect("/Admin/Error?message=Comment is not yours");

        var wsReg = await wsRefCommentsService.DeleteCommentAsync(commentId);
        var comments = wsReg is null ? string.Empty : $"?comments={wsReg.WsRefId}";
        return Redirect($"/WsRef{comments}");
    }
}

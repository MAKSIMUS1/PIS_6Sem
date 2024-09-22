using lab3b_vd.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;

namespace lab3b_vd.Attributes;

public class AuthorizeAsGuestIfNotAuthorizedAttribute : ActionFilterAttribute
{
    public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var httpContext = context.HttpContext;

        httpContext.Session.SetString("test", "good");

        var signInManager = httpContext.RequestServices.GetRequiredService<SignInManager<User>>();
        var userManager = httpContext.RequestServices.GetRequiredService<UserManager<User>>();

        if (signInManager.IsSignedIn(httpContext.User))
        {
            await next();
            return;
        }

        var guestUser = await userManager.Users.FirstOrDefaultAsync(u => u.UserName == "Guest");
        if (guestUser == null)
        {
            httpContext.Response.Redirect("/Admin/Error?message=Guest was not created");
            return;
        }

        await signInManager.SignInAsync(guestUser, true);
        await next();
    }
}

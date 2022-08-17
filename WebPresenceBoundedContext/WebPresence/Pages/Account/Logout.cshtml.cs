using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebPresence.Pages.Account;

public class LogoutModel : PageModel
{
    private readonly SignInManager<SiteUser> _signinManager;

    public LogoutModel(SignInManager<SiteUser> signinManager)
    {
        _signinManager = signinManager;
    }

    public async Task<ActionResult> OnPostAsync()
    {
        await _signinManager.SignOutAsync();
        return RedirectToPage("/Account/Login");
    }
}

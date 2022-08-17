using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace WebPresence.Pages.Account;

public class LoginModel : PageModel
{
    private readonly SignInManager<SiteUser> _signInManager;

    public LoginModel(SignInManager<SiteUser> signInManager)
    {
        _signInManager = signInManager;
    }

    [BindProperty]
    [Required]
    [EmailAddress]
    public string Username { get; set; } = "";

    [BindProperty]
    [Required]
    public string Password { get; set; } = "";

    [BindProperty]
    public bool RememberMe { get; set; } = false;


    public async Task OnGetAsync(string? returnUrl = null)
    {
        await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);
        ViewData["ReturnUrl"] = returnUrl;
        
    }

    public async Task<ActionResult> OnPostAsync(string? returnUrl = null)
    {
        var result = await _signInManager.PasswordSignInAsync(Username, Password, RememberMe, lockoutOnFailure: false);

        if(result.Succeeded)
        {
            return Redirect(returnUrl ?? "/");
        } else
        {
            ModelState.AddModelError(String.Empty, "Invalid Login Attempt");
            return Page();
        }
        
    }
}

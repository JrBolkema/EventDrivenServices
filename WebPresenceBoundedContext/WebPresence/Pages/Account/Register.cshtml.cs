using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace WebPresence.Pages.Account;

public class Register : PageModel
{

    private readonly UserManager<SiteUser> _userManager;
    private readonly SignInManager<SiteUser> _signInManager;
    private readonly DaprPubSubAdapter _dapr;

    public Register(UserManager<SiteUser> userManager, SignInManager<SiteUser> signInManager, DaprPubSubAdapter dapr)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _dapr = dapr;
    }

    [BindProperty]
    [Required]
    public string FirstName { get; set; } = string.Empty;

    [BindProperty]
    [Required]
    public string LastName { get; set; } = string.Empty;

    [BindProperty]
    [Required]
    public string Password { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    [BindProperty] public string Email { get; set; } = string.Empty;
    [Required]
    [BindProperty]
    public string ConfirmPassword { get; set; } = string.Empty;
    public void OnGet()
    {

    }

    public async Task<ActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }
        if (Password != ConfirmPassword)
        {
            ModelState.AddModelError("password", "Passwords must match");
            return Page();
        }
        var siteUser = new SiteUser()
        {
            Email = Email,
            UserName = Email,
            FirstName = FirstName,
            LastName = LastName

        };
        var result = await _userManager.CreateAsync(siteUser, Password);
        await _signInManager.SignInAsync(siteUser, isPersistent: false);
        await _dapr.NotifyOfNewUserAsync(siteUser);
        return Redirect("/");
    }
}

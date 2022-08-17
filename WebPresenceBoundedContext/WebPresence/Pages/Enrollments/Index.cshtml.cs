using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebPresence.Pages.Enrollments;

[Authorize]
public class IndexModel : PageModel
{
    private readonly EnrollmentsManager _enrollmentsManager;
    private readonly UserManager<SiteUser> _userManager;

    public IndexModel(EnrollmentsManager enrollmentsManager, UserManager<SiteUser> userManager)
    {
        _enrollmentsManager = enrollmentsManager;
        _userManager = userManager;
    }
    [BindProperty]
    public EnrollmentsSummary Summary { get; set; } = new();
    public async Task OnGetAsync()
    {
        var userId = _userManager.GetUserId(User);

        Summary = await _enrollmentsManager.GetEnrollmentsForUserAsync(userId);
        
    }
}

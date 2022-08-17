using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebPresence.Pages.Enrollments
{
    [Authorize]
    public class NewModel : PageModel
    {
        private readonly UserManager<SiteUser> _userManager;
        private readonly ILogger<NewModel> _logger;
        private readonly EnrollmentsManager _enrollmentsManager;

        public NewModel(UserManager<SiteUser> userManager, ILogger<NewModel> logger, EnrollmentsManager enrollmentsManager)
        {
            _userManager = userManager;
            _logger = logger;
            _enrollmentsManager = enrollmentsManager;
        }

        [BindProperty]
        public bool AlreadyEnrolled { get; set; }

        [BindProperty]
        public CourseEntity Course { get; set; } = new();

        [BindProperty]
        public OfferingEntity Offering { get; set; } = new();

        [BindProperty]
        public string OfferingId { get; set; } = string.Empty;

        [BindProperty]
        public string CourseId { get; set; } = string.Empty;


        public async Task<ActionResult> OnGetAsync(string? offeringId = null)
        {
            // check to see if this user is already enrolled in the course.
           if(offeringId is null)
            {
                return Redirect("/Courses");
            }
            var userId = _userManager.GetUserId(User);
     
            // if not, get the course and offering.
            AlreadyEnrolled = await _enrollmentsManager.CheckForAlreadyEnrolledAsync(offeringId, userId);
            if(AlreadyEnrolled)
            {
                return Page();
            } else
            {
                var info = await _enrollmentsManager.GetOfferingDetails(offeringId);
                Course = info.Course;
                Offering = info.Offering;
                CourseId = info.Course.Id;

                return Page();
            }

            // IF the offering has no seats, offer to put them on standby


        }

        public async Task<ActionResult> OnPostAsync()
        {
            _logger.LogInformation(OfferingId);
            var userId = _userManager.GetUserId(User);
            await _enrollmentsManager.CreateEnrollmentAsync(userId, OfferingId, CourseId);
            return Redirect("/Enrollments");
        }
    }
}

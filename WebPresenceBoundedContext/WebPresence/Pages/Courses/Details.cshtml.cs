
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebPresence.Pages.Courses;

public class DetailsModel : PageModel
{
    private readonly CourseManager _courseManager;

    public DetailsModel(CourseManager courseManager)
    {
        _courseManager = courseManager;

    }

    [BindProperty]
    public CourseDetails CourseDetails { get; set; } = new();

    public async Task<ActionResult> OnGetAsync(string courseId)
    {
        CourseDetails = await _courseManager.GetCourseDetailsAsync(courseId);
        if (CourseDetails.Course is null)
        {
            return NotFound();
        }
        else
        {
            return new PageResult();
        }
    }
}

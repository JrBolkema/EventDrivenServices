using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebPresence.Pages.Courses;

public class CoursesIndexModel : PageModel
{
    private readonly CourseManager _courseManager;

    public CoursesIndexModel(CourseManager courseManager)
    {
        _courseManager = courseManager;
    }

    [BindProperty]
    public List<CourseSummaryItem> Courses { get; set; } = new();
    public async Task OnGetAsync()
    {
        Courses = await _courseManager.GetCourseListAsync();
    }
}

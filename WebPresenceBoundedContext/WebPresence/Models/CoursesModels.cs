namespace WebPresence.Models;


public record CourseSummaryItem(string Id, string Title, int NumberOfDays);


public record CourseDetails
{
    public CourseEntity? Course { get; set; }
    public List<OfferingEntity>? Offerings { get; set; }
}

public record OfferingDetails
{
    public CourseEntity Course { get; set; } = new();
    public OfferingEntity Offering { get; set; } = new();
}

public record EnrollmentsSummary
{
    public List<EnrollmentSummaryItem> Enrollments { get; set; } = new();
}

public record EnrollmentSummaryItem
{
    public string EnrollmentId { get; set; } = string.Empty;
    public string CourseTitle { get; set; } = string.Empty;
    public string CourseId { get; set; } = string.Empty;
    public DateTime EnrolledDate { get; set; }


    public EnrollmentStatus Status { get; set; }
}
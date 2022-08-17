
namespace WebPresenceMessages.Enrollments;

public record EnrollmentCreated
{
    public string EnrollmentId { get; init; } = string.Empty;
    public string UserId { get; init; } = string.Empty;
    public string OfferingId { get; init; } = string.Empty;
    public string CourseId { get; init; } = string.Empty;
    public DateTime Created { get; init; }


    public static string TopicName => "webpresence-internal-enrollment-request-created";
}

public record EnrollmentStatusChange
{
    public string EnrollmentId { get; init; } = string.Empty;
    public string UserId { get; init; } = string.Empty;
    public string Reason { get; init; } = string.Empty;

}
public record EnrollmentRequestRejected : EnrollmentStatusChange
{

    public static string TopicName => "webpresence-internal-enrollment-request-rejected";
}

public record EnrollmentRequestCancelled : EnrollmentStatusChange
{

    public static string TopicName => "webpresence-internal-enrollment-request-cancelled";
}

public record EnrollmentRequestApproved :EnrollmentStatusChange
{

    public static string TopicName => "webpresence-internal-enrollment-request-approved";
}
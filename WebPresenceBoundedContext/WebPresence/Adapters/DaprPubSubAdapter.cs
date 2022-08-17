using Dapr.Client;
using WebPresenceMessages.Enrollments;
using WebPresenceMessages.Users;

namespace WebPresence.Adapters;

public class DaprPubSubAdapter
{

    private readonly DaprClient _daprClient;

    public DaprPubSubAdapter(DaprClient daprClient)
    {
        _daprClient = daprClient;
    }

    public async Task NotifyOfNewUserAsync(SiteUser user)
    {
        var newUser = new NewUserCreated
        {
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            UserId = user.Id.ToString()
        };
    
        await _daprClient.PublishEventAsync("webpresence", NewUserCreated.TopicName, newUser);
    }

    public async Task NotifyOfEnrollmentAsync(EnrollmentEntity enrollment)
    {
        var newEnrollment = new EnrollmentCreated
        {
            EnrollmentId = enrollment.Id.ToString(),
            CourseId = enrollment.CourseId,
            OfferingId = enrollment.OfferingId,
            Created = enrollment.Created,
            UserId = enrollment.UserId
        };

        await _daprClient.PublishEventAsync("webpresence", EnrollmentCreated.TopicName, newEnrollment);
    }
}


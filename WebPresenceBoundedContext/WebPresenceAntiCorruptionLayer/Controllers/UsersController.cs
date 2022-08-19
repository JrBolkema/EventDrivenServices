using System.Reflection.Metadata.Ecma335;

using Confluent.Kafka;

using Dapr;

using Google.Protobuf.WellKnownTypes;

using Hypertheory.Events;

using Microsoft.AspNetCore.Mvc;

using WebPresenceAntiCorruptionLayer.Producers;

using WebPresenceMessages.Enrollments;
using WebPresenceMessages.Users;

namespace WebPresenceAntiCorruptionLayer.Controllers;
[ApiController]
public class UsersController : ControllerBase
{
    private readonly DomainUserOnboardedEventProducer _producer;
    private readonly DomainEnrollmentRequestEventProducer _domainEnrollmentRequestEventProducer;
    public UsersController(DomainUserOnboardedEventProducer producer, DomainEnrollmentRequestEventProducer domainEnrollmentRequestEventProducer)
    {
        _producer = producer;
        _domainEnrollmentRequestEventProducer = domainEnrollmentRequestEventProducer;
    }
    //when there is an internal user created, public it as a domain user onboarded

    [Topic("webpresence-acl-dev", "webpresence-internal-enrollment-request-created")]
    [HttpPost("/webpresence/acl/map-enrollment-request-to-domain")]
    public async Task<ActionResult> MapIt(EnrollmentCreated request)
    {
        var messageToPublish = new Hypertheory.Events.EnrollmentRequested
        {
            OfferingId = request.OfferingId,
            StudentId = request.UserId,
            Originator = "web-presence"
            
        };

        var message = new Message<Null, EnrollmentRequested>
        {
            Value = messageToPublish
        };
        await _domainEnrollmentRequestEventProducer.ProduceAsync("hypertheory-events-enrollment-requested", message);
        return Ok();
    }




    [Topic("webpresence-acl-dev", "webpresence-internal-user-created")]
    [HttpPost("/webpresence/acl/map-new-user-to-domain")]
    public async Task<ActionResult> MapOnboard(NewUserCreated request)
    {
        var messageToPublish = new Hypertheory.Events.UserOnboarded
        {
            Email = request.Email,
            FirstName = request.FirstName,
            LastName = request.LastName,
            Source = "web-presence",
            UserId = request.UserId
        };

        var mesage = new Message<Null, UserOnboarded>
        {
            Value = messageToPublish
        };
        await _producer.ProduceAsync("hypertheory-events-useronboarded", mesage);
        return Ok();
    }
}

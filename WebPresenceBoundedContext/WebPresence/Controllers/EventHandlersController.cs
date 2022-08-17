using Dapr;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using WebPresenceMessages.Enrollments;
using WebPresenceMessages.Users;

namespace WebPresence.Controllers;

public class EventHandlersController : ControllerBase
{
    private readonly UserManager<SiteUser> _userManager;
    private readonly MongoDbWebAdapter _adapter;
    public EventHandlersController(UserManager<SiteUser> userManager, MongoDbWebAdapter adapter)
    {
        _userManager = userManager;
        _adapter = adapter;
    }

    [Topic("web-presence", "webpresence-internal-user-deactivated")]

    [HttpPost("/event-handlers/user-deactivated")]
    public async Task<ActionResult> DeactivateUserAsync([FromBody] UserDeactivated request)
    {
        var user = await _userManager.FindByIdAsync(request.UserId);
        if (user != null)
        {
            await _userManager.SetLockoutEnabledAsync(user, true);
            return Ok();
        }
        else
        {
            return BadRequest();
        }
    }


    [Topic("web-presence", "webpresence-internal-enrollment-request-rejected")]
    [HttpPost("/event-handlers/enrollment-rejected")]
    public async Task<ActionResult> RejectEnrollmentAsync([FromBody] EnrollmentRequestRejected request)
    {

        var newStatus = EnrollmentStatus.Denied;
        return await UpdateEnrollmentStatusAsync(request, newStatus);
    }

    [Topic("web-presence", "webpresence-internal-enrollment-request-approved")]
    [HttpPost("/event-handlers/enrollment-rejected")]
    public async Task<ActionResult> AcceptEnrollmentAsync([FromBody] EnrollmentRequestApproved request)
    {

        var newStatus = EnrollmentStatus.Approved;
        return await UpdateEnrollmentStatusAsync(request, newStatus);
    }



    private async Task<ActionResult> UpdateEnrollmentStatusAsync(EnrollmentStatusChange request, EnrollmentStatus newStatus)
    {
        var requestedId = ObjectId.Parse(request.EnrollmentId);
        var filter = Builders<EnrollmentEntity>.Filter.Where(e => e.Id == requestedId);
        var enrollment = await _adapter.Enrollments.Find(filter).SingleOrDefaultAsync();
        if (enrollment is null)
        {
            return BadRequest();
        }
        else
        {
            enrollment.EnrollmentStatus = newStatus;
            enrollment.DeniedReason = request.Reason ?? "No Reason Given";
            await _adapter.Enrollments.ReplaceOneAsync(filter, enrollment);
            return Ok();
        }
    }
}

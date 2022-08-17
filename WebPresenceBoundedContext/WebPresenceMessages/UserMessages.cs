

namespace WebPresenceMessages.Users;

public record NewUserCreated
{
    public string FirstName { get; init; } = string.Empty;
    public string LastName { get; init; } = string.Empty;
    public string UserId { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    public DateTime Created { get; init; } = DateTime.Now;

    public static string TopicName => "webpresence-internal-user-created";
}

public record UserDeactivated
{
    public string UserId { get; init; } = string.Empty;
    public DateTime WhenDeactivated { get; init; } = DateTime.Now;
    public static string TopicName => "webpresence-internal-user-deactivated";
}


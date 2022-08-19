using Confluent.Kafka;

using Hypertheory.Documents;
using Hypertheory.Events;
using Hypertheory.KafkaUtils.Consumers;
using pb = Google.Protobuf.WellKnownTypes;
using UsersAcl.Producers;
using System.Text;

namespace UsersAcl.Consumers;

public class UserOnboardedConsumer : ConsumeEventProtobufBackgroundService<UserOnboarded>
{
    private readonly UserProducer _producer;
    private readonly ILogger<UserOnboardedConsumer> _logger;
    private readonly string _topic;

    public UserOnboardedConsumer(ConsumerConfig consumerConfig, UserProducer producer, ILogger<UserOnboardedConsumer> logger, string topic) : base(consumerConfig, topic)
    {
        _producer = producer;
        _logger = logger;
        _topic = topic;
    }

    protected override void HandleConsumeLoop(ConsumeResult<Null, UserOnboarded> result)
    {
        var incomingMessage = result.Message.Value;

        var outgoingUser = new User
        {
            AccountRepId = "GoodOlStacey@email.com",
            Email = incomingMessage.Email,
            FirstName = incomingMessage.FirstName,
            LastName = incomingMessage.LastName,
            UserId = incomingMessage.UserId,
            UserSince = pb.Timestamp.FromDateTime(DateTime.Now.ToUniversalTime())
        };

        var outgoingKey = new UserKey { Id = outgoingUser.UserId };

        var originHeaderValue = "users-acl-service";

        var message = new Message<UserKey, User>
        {
            Key = outgoingKey,
            Value = outgoingUser,
            Headers = new Headers { new Header("origin-service", Encoding.UTF8.GetBytes(originHeaderValue)) }
        };
    }
}

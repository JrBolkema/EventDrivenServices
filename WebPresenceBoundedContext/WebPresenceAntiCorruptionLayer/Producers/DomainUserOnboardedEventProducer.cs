using Confluent.SchemaRegistry.Serdes;

using Hypertheory.Events;
using Hypertheory.KafkaUtils.Handlers;
using Hypertheory.KafkaUtils.Producers;

namespace WebPresenceAntiCorruptionLayer.Producers;

public class DomainUserOnboardedEventProducer : EventProducer<UserOnboarded, ProtobufSerializer<UserOnboarded>>
{
    public DomainUserOnboardedEventProducer(ClientHandle handler, string registryUrl) : base(handler, registryUrl)
    {
    }
}

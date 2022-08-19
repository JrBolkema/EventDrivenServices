using Confluent.SchemaRegistry.Serdes;

using Hypertheory.Events;
using Hypertheory.KafkaUtils.Handlers;
using Hypertheory.KafkaUtils.Producers;

namespace WebPresenceAntiCorruptionLayer.Producers;

public class DomainEnrollmentRequestEventProducer : EventProducer<EnrollmentRequested, ProtobufSerializer<EnrollmentRequested>>
{
    public DomainEnrollmentRequestEventProducer(ClientHandle handler, string registryUrl) : base(handler, registryUrl)
    {
    }
}




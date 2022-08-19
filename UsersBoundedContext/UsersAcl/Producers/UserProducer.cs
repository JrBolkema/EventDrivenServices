using Confluent.SchemaRegistry.Serdes;

using Hypertheory.Documents;
using Hypertheory.KafkaUtils.Handlers;
using Hypertheory.KafkaUtils.Producers;

namespace UsersAcl.Producers;

public class UserProducer : DocumentProducer<UserKey, User, ProtobufSerializer<UserKey>, ProtobufSerializer<User>>
{
    public UserProducer(ClientHandle handler, string registryUrl) : base(handler, registryUrl)
    {
    }
}

using Confluent.Kafka;

using Hypertheory.KafkaUtils.Handlers;

using Microsoft.Extensions.Options;

using WebPresenceAntiCorruptionLayer.Producers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddOptions<ProducerConfig>().Bind(builder.Configuration.GetSection("Kafka:ProducerSettings"));
var schemaUrl = builder.Configuration.GetConnectionString("schema-registry");

builder.Services.AddSingleton<ClientHandle>(sp =>
{
    var config = sp.GetRequiredService<IOptions<ProducerConfig>>().Value;
    return new ClientHandle(config);
});

builder.Services.AddSingleton<DomainUserOnboardedEventProducer>(s =>{
    var hangle = s.GetRequiredService<ClientHandle>();
    return new DomainUserOnboardedEventProducer(hangle, schemaUrl);
});

builder.Services.AddSingleton<DomainEnrollmentRequestEventProducer>(s => {
    var hangle = s.GetRequiredService<ClientHandle>();
    return new DomainEnrollmentRequestEventProducer(hangle, schemaUrl);
});
builder.Services.AddControllers();

var app = builder.Build();
app.UseCloudEvents();


// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();
app.MapSubscribeHandler();

app.Run();

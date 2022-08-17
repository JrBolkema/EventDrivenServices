using MongoDB.Bson.Serialization.Conventions;
using AspNetCore.Identity.Mongo;


var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("mongodb");

var conventionPack = new ConventionPack()
{
        new CamelCaseElementNameConvention(),
        new IgnoreExtraElementsConvention(true),
        new EnumRepresentationConvention(MongoDB.Bson.BsonType.String),
};
ConventionRegistry.Register("default", conventionPack, t => true);


builder.Services.AddIdentityMongoDbProvider<SiteUser>(identity =>
    {
        identity.Password.RequireDigit = false;
        identity.Password.RequireLowercase = false;
        identity.Password.RequireNonAlphanumeric = false;
        identity.Password.RequireUppercase = false;
        identity.Password.RequiredLength = 1;
        identity.Password.RequiredUniqueChars = 0;
    },
    mongo =>
    {
        mongo.ConnectionString = connectionString + "/web_db?authSource=admin";

    }
);


// Add services to the container.
builder.Services.AddDaprClient();
builder.Services.AddRazorPages();


builder.Services.AddSingleton(new MongoDbWebAdapter(connectionString));

builder.Services.AddScoped<CourseManager>();
builder.Services.AddScoped<EnrollmentsManager>();
builder.Services.AddScoped<DaprPubSubAdapter>();
builder.Services.AddControllers();


var app = builder.Build();
app.UseCloudEvents();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapSubscribeHandler();
app.MapRazorPages();

app.Run();

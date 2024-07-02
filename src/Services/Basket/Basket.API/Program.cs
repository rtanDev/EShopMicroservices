using HealthChecks.UI.Client;

var builder = WebApplication.CreateBuilder(args);

//Add services to the container
var assembly = typeof(Program).Assembly;
var redisConfig = builder.Configuration.GetConnectionString("Redis");
var dbConfig = builder.Configuration.GetConnectionString("Database");

builder.Services.AddCarter();
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(assembly);
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
    config.AddOpenBehavior(typeof(LoggingBehavior<,>));
});
builder.Services.AddMarten(opts =>
{
    opts.Connection(dbConfig!);
    opts.Schema.For<ShoppingCart>().Identity(i => i.Username);
}).UseLightweightSessions();

builder.Services.AddScoped<IBasketRepository, BasketRepository>();
builder.Services.Decorate<IBasketRepository, CachedBasketRepository>();

/* Not scallable decoration
builder.Services.AddScoped<IBasketRepository>(provider =>
{
    var basketRepo = provider.GetRequiredService<BasketRepository>();
    return new CachedBasketRepository(basketRepo, provider.GetRequiredService<IDistributedCache>());
});
*/


builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = redisConfig;
    //options.InstanceName = "Basket";
});

builder.Services.AddExceptionHandler<CustomExceptionHandler>();

builder.Services.AddHealthChecks()
    .AddNpgSql(dbConfig!)
    .AddRedis(redisConfig!);

var app = builder.Build();

//Configure the HTTP request pipeline
app.MapCarter();

app.UseExceptionHandler(options => { });

app.UseHealthChecks("/health",
    new HealthCheckOptions
    {
        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
    });

app.Run();

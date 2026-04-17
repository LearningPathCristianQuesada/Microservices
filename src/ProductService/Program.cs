var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMarten(opts =>
{
    opts.Connection(builder.Configuration.GetConnectionString("Database"));
}).UseLightweightSessions();

var app = builder.Build();

app.Run();
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

// Builder Extensions
builder.AddData();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

// Endpoint Extensions
app.MapEndpoints();

app.Run();

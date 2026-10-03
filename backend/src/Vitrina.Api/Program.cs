using Vitrina.Api.Middleware;
using Vitrina.Dto.Common;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

var app = builder.Build();

app.UseMiddleware<GlobalExceptionHandlingMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.MapGet("/", () => Results.Ok(ValidationResultDto.Success("Vitrina API is running.")));

app.Run();

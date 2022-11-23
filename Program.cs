using CustomerRankService.Business.Interfaces;
using CustomerRankService.Business.Services;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(s => {
    s.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Customer Rank Service",
        Version = "v1",
        Description = "Powered by .NET 6.0"
    });
});

builder.Services.AddScoped<ILeaderboardService, LeaderboardService>();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();

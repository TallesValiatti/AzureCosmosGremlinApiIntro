using AzureCosmosGremlinApiIntro.Services;
using AzureCosmosGremlinApiIntro.Services.Interfaces;
using Contexts;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<GraphContext>();
builder.Services.AddScoped<IGraphRepository, GraphRepository>();
builder.Services.AddScoped<ISignInValidatorService, SignInValidatorService>();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

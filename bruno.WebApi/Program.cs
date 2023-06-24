using bruno.Application;
using bruno.Infrastructure;
using bruno.BankSystem.WebApi;
using bruno.Presentation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services
    .AddWebApi()
    .AddApplication()
    .AddInfrastructure(builder.Configuration)
    .AddContracts();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(x => x
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader());

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
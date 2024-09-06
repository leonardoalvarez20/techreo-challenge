using TechreoChallenge.Endpoints;
using TechreoChallenge.Api.Settings;
using MongoDB.Driver;
using Microsoft.Extensions.Options;
using TechreoChallenge.Services;
using TechreoChallenge.Data.Repositories;
using TechreoChallenge.Api.Extensions;
using TechreoChallenge.Api.Endpoints;
using TechreoChallenge.Api.Services;
using Microsoft.OpenApi.Models;
using TechreoChallenge.Api.Data.Repositories;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddJwtBearerSecurity();
});
builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddCors();
builder.Services.ConfigureJwt(builder.Configuration);
builder.Services.AddAuthorization();
builder.Services.ConfigureMongoDb(builder.Configuration);

//Register API services and repositories

builder.Services.AddScoped<ISavingsAccountService, SavingsAccountService>();
builder.Services.AddScoped<ISavingsAccountRepository, SavingsAccountRepository>();
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();
builder.Services.AddScoped<ITransactionService, TransactionService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(builder =>
{
    builder.AllowAnyOrigin()
           .AllowAnyHeader()
           .AllowAnyMethod();
});

app.UseAuthentication();
app.UseAuthorization();

//Map API endpoints
app.MapAuthenticationEndpoints();
app.MapCustomerEndpoints();
app.MapSavingsAccountsEndpoints();
app.MapTransactionsEndpoints();
app.Run();

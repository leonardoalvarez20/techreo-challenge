using TechreoChallenge.Endpoints;
using MongoDB.Driver;
using TechreoChallenge.Services;
using TechreoChallenge.Data.Repositories;
using TechreoChallenge.Api.Extensions;
using TechreoChallenge.Api.Endpoints;
using TechreoChallenge.Api.Services;
using TechreoChallenge.Api.Middleware;
using TechreoChallenge.Api.Data.Repositories;
using Microsoft.AspNetCore.HttpLogging;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddJwtBearerSecurity();
});
builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.ConfigureJwt(builder.Configuration);
builder.Services.AddAuthorization();
builder.Services.ConfigureMongoDb(builder.Configuration);
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});

builder.Services.AddHttpLogging(logging =>
{
    logging.LoggingFields = HttpLoggingFields.RequestMethod | HttpLoggingFields.RequestPath | HttpLoggingFields.RequestQuery | HttpLoggingFields.ResponseStatusCode;
});


//Register API services and repositories
builder.Services.AddScoped<ISavingsAccountService, SavingsAccountService>();
builder.Services.AddScoped<ISavingsAccountRepository, SavingsAccountRepository>();
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();
builder.Services.AddScoped<ITransactionService, TransactionService>();

var app = builder.Build();
//Create indexes
using (var scope = app.Services.CreateScope())
{
    var database = scope.ServiceProvider.GetRequiredService<IMongoDatabase>();
    MongoDbConfigurationExtension.CreateUniqueIndexes(database);
}
//Config logs
app.UseHttpLogging();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAllOrigins");

app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<ExceptionHandlingMiddleware>();

//Map API endpoints
app.MapAuthenticationEndpoints();
app.MapCustomerEndpoints();
app.MapSavingsAccountsEndpoints();
app.MapTransactionsEndpoints();
app.Run();

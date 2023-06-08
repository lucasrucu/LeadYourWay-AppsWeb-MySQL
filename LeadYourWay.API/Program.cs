using LeadYourWay.API.Mapper;
using LeadYourWay.Domain;
using LeadYourWay.Infrastructure;
using LeadYourWay.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Dependency Injection
builder.Services.AddScoped<IUserInfrastructure, UserMySQLInfrastructure>();
builder.Services.AddScoped<IUserDomain, UserDomain>();
builder.Services.AddScoped<IBicycleInfrastructure, BicycleMySQLInfrastructure>();
builder.Services.AddScoped<IBicycleDomain, BicycleDomain>();
builder.Services.AddScoped<ICardInfrastructure, CardMySQLInfrastructure>();
builder.Services.AddScoped<ICardDomain, CardDomain>();
builder.Services.AddAutoMapper(
    typeof(ModelToResponse),
    typeof(RequestToModel)
);

// CORS configuration
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowOrigin",
        builder =>
        {
            builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
        });
});

// MySQL Connection
var connectionString = builder.Configuration.GetConnectionString("LeadYourWayConnection");
var serverVersion = new MySqlServerVersion(new Version(8, 0, 29));

builder.Services.AddDbContext<LeadYourWayContext>(
    dbContextOptions =>
    {
        dbContextOptions.UseMySql(connectionString,
            ServerVersion.AutoDetect(connectionString),
            options => options.EnableRetryOnFailure(
                maxRetryCount: 5,
                maxRetryDelay: System.TimeSpan.FromSeconds(30),
                errorNumbersToAdd: null)
        );
    });

var app = builder.Build();
    
// Create database if not exists
using (var scope = app.Services.CreateScope())
using (var context = scope.ServiceProvider.GetService<LeadYourWayContext>())
{
    context.Database.EnsureCreated();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Enable CORS globally
app.UseCors("AllowOrigin");

app.UseAuthorization();

app.MapControllers();

app.Run();
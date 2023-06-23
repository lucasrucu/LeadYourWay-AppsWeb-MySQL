using LeadYourWay.API.Mapper;
using LeadYourWay.API.Middleware;
using LeadYourWay.Domain;
using LeadYourWay.Infrastructure;
using LeadYourWay.Infrastructure.Context;
using Microsoft.AspNetCore.Authentication.JwtBearer;
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
builder.Services.AddScoped<IRentInfrastructure, RentMySQLInfrastructure>();
builder.Services.AddScoped<IRentDomain, RentDomain>();
builder.Services.AddScoped<ITokenDomain, TokenDomain>();
builder.Services.AddScoped<IEncryptDomain, EncryptDomain>();
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

// JWT
/*builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
});*/

builder.Services.AddDbContext<LeadYourWayContext>(
    dbContextOptions =>
    {
        dbContextOptions.UseMySql(connectionString,
            ServerVersion.AutoDetect(connectionString),
            options => options.EnableRetryOnFailure(
                5,
                TimeSpan.FromSeconds(30),
                null)
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
app.UseCors(x => x
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

/*app.UseMiddleware<JwtMiddleware>();*/

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
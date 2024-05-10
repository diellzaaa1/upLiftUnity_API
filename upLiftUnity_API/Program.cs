using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Stripe;
using System.Text;
using upLiftUnity_API.Controllers;
using upLiftUnity_API.Models;
using upLiftUnity_API.Repositories.ActivitiesRepository;
using upLiftUnity_API.Repositories.ApplicationRepository;
using upLiftUnity_API.Repositories.DonationRepository;
using upLiftUnity_API.Repositories.ScheduleRepository;
using upLiftUnity_API.Repositories.UserRepository;

using upLiftUnity_API.MongoModels;
using upLiftUnity_API.Services.EmailSender;




var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

StripeConfiguration.ApiKey = builder.Configuration["Stripe:SecretKey"];



//Dependency Injection of DBcontext Class 
builder.Services.AddDbContext<APIDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DevConnection")));
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IApplicationRepository, ApplicationRepository>();
builder.Services.AddScoped<IDonationRepository, DonationRepository>();
builder.Services.AddScoped<IScheduleRepository, ScheduleRepository>();
builder.Services.AddScoped<IActivitiesRepository, ActivitiesRepository>();
builder.Services.AddSingleton<MongoDbContext>();
builder.Services.AddScoped<IEmailSender,EmailSender>();




builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("OUR_SECRET_KEY_FROM_EKIPA_SHKATERRUSE")),
            ValidateIssuer = false,
            ValidateAudience = false,
            ClockSkew = TimeSpan.Zero
        };
    });
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Authentication Token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JsonWebToken",
        Scheme = "Bearer"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});






var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(
    options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()
);

app.UseAuthentication();
app.UseHttpsRedirection();

app.UseAuthorization();



app.MapControllers();

app.Run();

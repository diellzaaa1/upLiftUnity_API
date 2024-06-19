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
using Microsoft.AspNetCore.SignalR;
using upLiftUnity_API.MongoModels;
using upLiftUnity_API.Services.EmailSender;
using upLiftUnity_API.Repositories.NotificationRepository;
using upLiftUnity_API.Services;
using upLiftUnity_API.RealTimeChat.Hubs;
using upLiftUnity_API.RealTimeChat.Repositories;
using upLiftUnity_API.RealTimeChat.Repository.MessageRepository;
using upLiftUnity_API.RealTimeChat.Services;
using upLiftUnity_API.Repositories;
using upLiftUnity_API.Repositories.PlanetsRepository;






var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSignalR();

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
builder.Services.AddScoped<NotificationHub>();
builder.Services.AddSignalR();
builder.Services.AddScoped<NotificationService>();
builder.Services.AddScoped<IConversationRepository, ConversationRepository>();
builder.Services.AddScoped<IMessageRepository, MessageRepository>();
builder.Services.AddTransient<INotificationRepository, NotificationRepo>();
builder.Services.AddScoped<IRulesRepository, RulesRepository>();
builder.Services.AddScoped<IFeedbackRepository, FeedbackRepository>();

builder.Services.AddScoped<IPlanetRepository, PlanetRepository>();

// Register the background service
builder.Services.AddSingleton<MessageBufferService>();


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

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", builder =>
       builder
           .WithOrigins("http://localhost:8080", "http://localhost:8081", "http://localhost:8082", "http://localhost:8083", "http://localhost:8084") 
           .AllowAnyMethod()
           .AllowAnyHeader()
           .AllowCredentials()); 
});



var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("CorsPolicy");
app.UseAuthentication();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.MapHub<NotificationHub>("/notificationHub");
app.MapHub<ChatHub>("Chat");
app.Run();

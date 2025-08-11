using Gutenburg_Server.Repositories;
using Gutenburg_Server.Services;
using Microsoft.EntityFrameworkCore;
using Gutenburg_Server.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

using System.Text;

var builder = WebApplication.CreateBuilder(args);

var allowedOrigins = new[]
{
    "http://localhost:5173",
    "https://delightful-begonia-c03326.netlify.app",
    "http://localhost:3000"
};

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigins", policy =>
    {
        policy.WithOrigins(allowedOrigins)
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IJobRepository, JobRepository>();
builder.Services.AddScoped<IMessageRepository, MessageRepository>();
builder.Services.AddScoped<IMeetingRequestRepository, MeetingRequestRepository>();
builder.Services.AddScoped<IContentRepository, ContentRepository>();
builder.Services.AddScoped<IServiceRepository, ServiceRepository>();
builder.Services.AddScoped<ISolutionRepository, SolutionRepository>();
builder.Services.AddScoped<IApplicationRepository, ApplicationRepository>();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IJobService, JobService>();
builder.Services.AddScoped<IApplicationService, ApplicationService>();
builder.Services.AddScoped<IMessageService, MessageService>();
builder.Services.AddScoped<IMeetingRequestService, MeetingRequestService>();
builder.Services.AddScoped<IContentService, ContentService>();
builder.Services.AddScoped<IServiceService, ServiceService>();
builder.Services.AddScoped<ISolutionService, SolutionService>();

builder.Services.AddScoped<JwtService>();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("GutenburgDatabase")));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),

            RoleClaimType = ClaimTypes.Role
        };
    });


builder.Services.AddAuthorization();

var app = builder.Build();

app.UseCors("AllowSpecificOrigins");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();

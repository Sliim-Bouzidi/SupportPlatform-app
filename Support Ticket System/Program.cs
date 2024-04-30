using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Support_Ticket_System.DataContext;
using Support_Ticket_System.Services;
using Support_Ticket_System.Services.Commentservices;
using Support_Ticket_System.Services.PriorityServices;
using Support_Ticket_System.Services.ProcessFlowServices;
using Support_Ticket_System.Services.severity_services;
using Support_Ticket_System.Services.status_services;
using Support_Ticket_System.Services.Tagservices;
using Support_Ticket_System.Services.TenantServices;
using Support_Ticket_System.Services.ticketservices;
using Support_Ticket_System.Services.User_Services;
using Swashbuckle.AspNetCore.Filters;
using System.Text;
using System.Text.Json.Serialization;
using static System.Net.WebRequestMethods;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddCors();
builder.Services.AddDbContext<Datacontext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddScoped<IStatusservice, statusservices>();
builder.Services.AddScoped<ITicketService, ticketservices>();
builder.Services.AddScoped<IPriorityServices, priorityServices>();
builder.Services.AddScoped<IseverityServices, SeverityServices>();
builder.Services.AddScoped<ITagServices, TagServices>();
builder.Services.AddScoped<IUserServices, Userservices>();
builder.Services.AddScoped<IProcessFlowServices, ProcessFlowServices>();
builder.Services.AddScoped<ITenantServices, TenantServices>();
builder.Services.AddScoped<ICommentServices, CommentServices>();


builder.Services.AddControllersWithViews()
                .AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = "Standard Authorization header using Bearer scheme (\"bearer token\")",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey

    });
    options.OperationFilter<SecurityRequirementsOperationFilter>();
});

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;


}).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
               builder.Configuration.GetSection("AppSettings:Token").Value)),
        ValidateAudience = false,
        ValidateIssuer = false 

    };
});
builder.Services.AddAuthorization();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors(options => options
    .WithOrigins(new[] { "http://localhost:4200"})
    .AllowAnyHeader()
    .AllowAnyMethod()
    .AllowCredentials()
);
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();

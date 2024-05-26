//using FlatRockProject.Application.Services.Roles;
using FlatRockProject.Infrastructure.Config;
using FlatRockProject.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Filters;
using FlatRockProject.Application.Services.Quiz;
using FlatRockProject.Infrastructure.Config;
using FlatRockProject.Application.Services.Author;
using FlatRockProject.Application.Identity;
using FlatRockProject.Application;
using FlatRockProject.Application.Services.User;
using FlatRockProject.API.Config;
using System.Reflection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using System.Text;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using FlatRockProject.Infrastructure.Entities;
using FlatRockProject.Application.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<FRPDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionString"));
});
//builder.Services.AddJwtIdentity(builder.Configuration.GetSection(nameof(JwtConfiguration)));

builder.Services.AddIdentity<User, IdentityRole>()
        .AddEntityFrameworkStores<FRPDbContext>();
//builder.Services.AddJwtIdentity(builder.Configuration.GetSection(nameof(JwtConfiguration)));
var signingKey = new SymmetricSecurityKey(
                Encoding.Default.GetBytes(builder.Configuration["JwtConfiguration:Secret"]));

var tokenValidationParameters = new TokenValidationParameters
{
    ValidateIssuer = true,
    ValidIssuer = builder.Configuration["JwtConfiguration:Issuer"],

    ValidateAudience = true,
    ValidAudience = builder.Configuration["JwtConfiguration:Audience"],

    ValidateIssuerSigningKey = true,
    IssuerSigningKey = signingKey,

    RequireExpirationTime = false,
    ValidateLifetime = true,
    ClockSkew = TimeSpan.Zero
};

builder.Services.Configure<JwtConfiguration>(options =>
{
    options.Issuer = builder.Configuration["JwtConfiguration:Issuer"];
    options.Audience = builder.Configuration["JwtConfiguration:Audience"];
    options.SigningCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
});

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(configureOptions =>
{
    configureOptions.ClaimsIssuer = builder.Configuration["JwtConfiguration:Issuer"];
    configureOptions.TokenValidationParameters = tokenValidationParameters;
    configureOptions.SaveToken = true;
});

builder.Services.AddAuthorization(options =>
{
    options.DefaultPolicy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
});
//builder.Services.AddAutoMapper(typeof(UsersService));

builder.Services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
{
    builder
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader();
}));
//builder.Services.AddScoped<IUserRepository, UserRepository>();
//builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddTransient<IJwtFactory, JwtFactory>();
builder.Services.AddTransient<IRandomGenerator, RandomGenerator>();
builder.Services.AddTransient<IUsersService, UsersService>();
builder.Services.AddTransient<IQuizQuestionService, QuizQuestionService>();
builder.Services.AddTransient<IAuthorService, AuthorService>();
builder.Services.AddTransient<IEmailSender<IdentityUser>, NullEmailSender>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey
    });
    options.OperationFilter<SecurityRequirementsOperationFilter>();
});

var app = builder.Build();

app.MapIdentityApi<IdentityUser>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("MyPolicy");
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

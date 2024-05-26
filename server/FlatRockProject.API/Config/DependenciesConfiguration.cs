//using FlatRockProject.Infrastructure.Config;
//using FlatRockProject.Infrastructure.Entities;
//using FlatRockProject.Infrastructure.Persistence;
//using Microsoft.AspNetCore.Authentication.JwtBearer;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.IdentityModel.Tokens;
//using Microsoft.OpenApi.Models;
//using System.Text;

//namespace FlatRockProject.API.Config
//{
//    public static class DependenciesConfiguration
//    {
//        public static void AddDbContext(this IServiceCollection services, string connectionString)
//        {
//            if (string.IsNullOrEmpty(connectionString))
//            {
//                throw new ArgumentException(nameof(connectionString));
//            }

//            services
//                .AddDbContext<FRPDbContext>(opts => opts.UseSqlServer(connectionString))
//                .AddEntityFrameworkSqlServer();
//        }

//        public static void AddJwtIdentity(this IServiceCollection services, IConfigurationSection jwtConfiguration)
//        {
//            services.AddIdentity<User, IdentityRole>()
//                    .AddEntityFrameworkStores<FRPDbContext>();

//            var signingKey = new SymmetricSecurityKey(
//                Encoding.Default.GetBytes(jwtConfiguration["Secret"]));

//            var tokenValidationParameters = new TokenValidationParameters
//            {
//                ValidateIssuer = true,
//                ValidIssuer = jwtConfiguration[nameof(JwtConfiguration.Issuer)],

//                ValidateAudience = true,
//                ValidAudience = jwtConfiguration[nameof(JwtConfiguration.Audience)],

//                ValidateIssuerSigningKey = true,
//                IssuerSigningKey = signingKey,

//                RequireExpirationTime = false,
//                ValidateLifetime = true,
//                ClockSkew = TimeSpan.Zero
//            };

//            services.Configure<JwtConfiguration>(options =>
//            {
//                options.Issuer = jwtConfiguration[nameof(JwtConfiguration.Issuer)];
//                options.Audience = jwtConfiguration[nameof(JwtConfiguration.Audience)];
//                options.SigningCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
//            });

//            services.AddAuthentication(options =>
//            {
//                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//            }).AddJwtBearer(configureOptions =>
//            {
//                configureOptions.ClaimsIssuer = jwtConfiguration[nameof(JwtConfiguration.Issuer)];
//                configureOptions.TokenValidationParameters = tokenValidationParameters;
//                configureOptions.SaveToken = true;
//            });

//            services.AddAuthorization(options =>
//            {
//                options.DefaultPolicy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
//            });
//        }

//        public static void AddSwagger(this IServiceCollection services)
//        {
//            services.AddSwaggerGen(setup =>
//            {
//                setup.SwaggerDoc("v1", new OpenApiInfo { Title = "FlatRockProject.Api", Version = "v1" });

//                setup.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme { In = ParameterLocation.Header, Description = "Enter 'Bearer {token}' (don't forget to add 'bearer') into the field below.", Name = "Authorization", Type = SecuritySchemeType.ApiKey });

//            });
//        }
//    }
//}

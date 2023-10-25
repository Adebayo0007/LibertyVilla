using Business.Repository.IRepository;
using DataAccess.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Newtonsoft.Json.Serialization;
using Microsoft.OpenApi.Models;
using LibertyVilla_WebApi.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Common;
using Stripe;
using NLog;
using System.IO;
//using Microsoft.AspNetCore.Mvc.Versioning;

namespace LibertyVilla_WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            builder.Logging.ClearProviders();
            builder.Logging.AddConsole();

            builder.Services.AddCors(a => a.AddPolicy("CorsPolicy", b =>
            {
                //b.WithOrigins("http://localhost:5000/")
                b.AllowAnyMethod()
                .AllowAnyOrigin()
                .AllowAnyHeader();

            }));
            // Add services to the container.
            var connection = builder.Configuration.GetConnectionString("LibertyVillaConnectionString");
            builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseMySql(connection, ServerVersion.AutoDetect(connection)
          ));

            /*builder.Services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            builder.Services.AddAuthentication(
                CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie();*/

            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            builder.Services.AddScoped<IHotelRoomRepository, HotelRoomRepository>();
            builder.Services.AddScoped<IHotelImageRepository, HotelImageRepository>();
            builder.Services.AddScoped<IApplicationUserRepository, ApplicationUserRepository>();
            builder .Services .AddScoped<IRoomOrderRepository, RoomOrderRepository>();
            builder.Services.AddScoped<IJWTAuthentication, JWTAuthentication>();
            builder.Services.AddRouting(option => option.LowercaseUrls = true);
            builder.Services.AddControllers().AddJsonOptions(opt => opt.JsonSerializerOptions.PropertyNamingPolicy = null)
                .AddNewtonsoftJson(opt =>
                {
                    opt.SerializerSettings.ContractResolver = new DefaultContractResolver();
                    opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                });
           /* builder.Services.AddApiVersioning(options =>
            {
                options.ReportApiVersions = true;
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ApiVersionReader = new HeaderApiVersionReader("api-version"); // Specifying the header name that holds the version information
            });*/

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Liberty Villa Project", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please Bearer and then token is the field",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey

                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement{
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}
                    }

                 });
            });

            var key = SD.Local_Token;
            builder.Services.AddSingleton<IJWTAuthentication>(new JWTAuthentication());

            builder.Services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };

                });

            StripeConfiguration.ApiKey = SD.Stripe_Api_Secrete_key;
            LogManager.Setup().LoadConfigurationFromFile(SD.NLog_WebApi_Config_File);

            var app = builder.Build();
            app.Logger.LogInformation("Application starting...");
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Liberty Villa v1"));
            }

            app.Logger.LogInformation("Adding Route...");
            app.UseRouting();
            app.UseHttpsRedirection();
            app.UseCors("CorsPolicy");
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}
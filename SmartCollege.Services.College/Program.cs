using Microsoft.AspNetCore.Authentication.Negotiate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer; // Add this using directive
using SmartCollege.Services.College.Data;
using SmartCollege.Services.College.Extensions;
using SmartCollege.Services.College.Services;

namespace SmartCollege.Services.College
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<CollegeDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddScoped<ITokenService, TokenService>();
            builder.Services.AddScoped<IAuthService, AuthService>();

            builder.Services.AddJwtAuthentication(builder.Configuration);

            //builder.Services.AddAuthentication(NegotiateDefaults.AuthenticationScheme)
            //    .AddNegotiate();

            //builder.Services.AddAuthorization(options =>
            //{
            //    // By default, all incoming requests will be authorized according to the default policy.
            //    options.FallbackPolicy = options.DefaultPolicy;
            //});

            var app = builder.Build();

            // Configure the HTTP request pipeline.
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
        }
    }
}

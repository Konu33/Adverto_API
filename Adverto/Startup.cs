using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Adverto.Data;
using Adverto.Filters;
using Adverto.Options;
using Adverto.Repositories;
using Adverto.Repositories.AuthRepository;
using Adverto.Repositories.CategoryRepository;
using Adverto.Repositories.ReportRepository;
using Adverto.Repositories.SubCategoryRepo;
using Adverto.Repositories.UserRepository;
using AutoMapper;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace Adverto
{
    public class Startup
    {
      
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddDbContext<DataContext>(options =>
               options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddMvc(opt => opt.Filters.Add<ValidationFilter>())
                .AddFluentValidation(mvc => mvc.RegisterValidatorsFromAssemblyContaining<Startup>());
            services.AddCors();

            var jwtSettings = new JwtSettings();
            Configuration.Bind(nameof(jwtSettings), jwtSettings);
            services.AddSingleton(jwtSettings);

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                                    .AddJwtBearer(options =>
                                    {
                                        options.TokenValidationParameters = new TokenValidationParameters
                                        {
                                            ValidateIssuerSigningKey = true,
                                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSettings.Secrect)),
                                            ValidateIssuer = false,
                                            ValidateAudience = false,

                                        };
                                    });
           
            services.AddScoped<IAdvertRepo, AdvertRepo>();
            services.AddScoped<IAuthRepository, AuthRepository>();
            services.AddScoped<ICategoryRepo, CategoryRepo>();
            services.AddScoped<IReportRepo, ReportRepo>();
            services.AddScoped<ISubCatergoryRepo, SubCategoryRepo>();
            services.AddScoped<IUserRepo, UserRepo>();
            services.AddAutoMapper(typeof(Startup));
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            app.UseHttpsRedirection();
            app.UseCors(builder => builder
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader()
   );
            app.UseRouting();
           
            app.UseAuthorization();
            app.UseAuthentication();
            app.UseStaticFiles();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

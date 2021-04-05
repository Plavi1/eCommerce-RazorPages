using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Stripovi.Data.Data;
using Stripovi.Data.Repositorys.KontaktRepository;
using Stripovi.Data.Repositorys.KorpaRepository;
using Stripovi.Data.Repositorys.PorudzbinaRepository;
using Stripovi.Data.Repositorys.StripRepository;
using Stripovi.Web.Profiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stripovi.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();

            services.AddScoped<IPorudzbinaRepository, SQLPorudzbinaRepository>();
            services.AddScoped<IStripRepository, SQLStripRepository>();
            services.AddScoped<IKorpaRepository, SQLKorpaRepository>();
            services.AddScoped<IKontaktRepository, SQLKontaktRepository>();

            services.Configure<RouteOptions>(options =>
            {
                options.LowercaseUrls = true;
                options.LowercaseQueryStrings = true;
                options.AppendTrailingSlash = true;
            });

            services.AddAutoMapper(typeof(StripProfile));

            services.AddDbContext<ApplicationDbContext>(options =>                                                                 //Kopirano iz IdentityHosta, jer je izbacivao error kada se Scaffolduje 
                    options.UseSqlServer(Configuration.GetConnectionString("ApplicationDbContextConnection"),
            x => x.MigrationsAssembly("Stripovi.Data")));

            services.AddDefaultIdentity<IdentityUser>(options => {
                options.SignIn.RequireConfirmedAccount = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;

            }).AddRoles<IdentityRole>()
              .AddEntityFrameworkStores<ApplicationDbContext>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");

                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }
}

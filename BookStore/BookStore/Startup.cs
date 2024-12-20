using BookStore.Data;
using BookStore.Models;
using BookStore.Repositary;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore
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

            services.AddDbContext<BookStoreContext>(options => options.UseSqlServer(Configuration.GetConnectionString("Default")));
            services.AddControllersWithViews();
            services.AddScoped<IBookRepositary, BookRepositary>();
            services.AddScoped<ILanguageRepositary, LanguageRepositary>();
            services.AddSingleton<IMessageRepositary, MessageRepositary>();
#if DEBUG
            services.AddRazorPages().AddRazorRuntimeCompilation();
            //    .AddViewOptions(option =>
            //{

            //    option.HtmlHelperOptions.ClientValidationEnabled = false;
            //});
#endif
            services.Configure<NewBookAlertConfig>("InternalBook", Configuration.GetSection("NewBookAlert"));
            services.Configure<NewBookAlertConfig>("ThirdPartyBook",Configuration.GetSection("ThirdBookAlert"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bookstore.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Bookstore
{
    public class Startup
    {
        //public Startup(IConfiguration configuration)
        //{
        //    Configuration = configuration;
        //}

        //public IConfiguration Configuration { get; }

        public IConfiguration Configuration { get; set; }

        public Startup(IConfiguration temp)
        {
            Configuration = temp;
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            services.AddDbContext<BookstoreContext>(options =>
            {
                options.UseSqlite(Configuration["ConnectionStrings:BookstoreDBConnection"]);
            });

             services.AddScoped<IBookstoreRepository, EFBookstoreRepository>();
             services.AddScoped<ICheckoutRepository, EFCheckoutRepository>();

            services.AddRazorPages();

            services.AddDistributedMemoryCache();
            services.AddSession();

            services.AddScoped<Basket>(x => SessionBasket.GetBasket(x));
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddServerSideBlazor();
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
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseSession();

            app.UseRouting();

            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("typepage",
                    // bookType name comes from Default.cshtml where we set it up as asp-route-Category
                    "{Category}/Page{pageNum}",// curly braces tell us that it will be a piece of information passed in 
                    new { Controller = "Home", action = "Index" });

                // use this endpoint to show page numbers in the URL in a clean way -- now it will only show "Page" and the number instead of ?pageNum=1
                endpoints.MapControllerRoute("Paging",
                    "Page{pageNum}", //pageNum is a variable 
                    new { Controller = "Home", action = "Index", pageNum = 1 }); // set default pageNum

                // endpoint if we are just passed a category
                endpoints.MapControllerRoute("type",
                    "{Category}",
                    new { Controller = "Home", action = "Index", pageNum = 1 });
                // we will default to the first page of that category if we are not passed a page number


                endpoints.MapDefaultControllerRoute(); // this is the default -- we could have also created a custom endpoint and told it exactly where to go

                endpoints.MapRazorPages();

                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/admin/{*catchall}", "/Admin/Index"); 

            });
        }
    }
}

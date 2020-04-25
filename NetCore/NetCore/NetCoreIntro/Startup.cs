using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace NetCoreIntro
{
    public class Startup
    {
      //Projeyi ilk olu�turdu�umuzda bu proje bir mvc projesi olarak olu�turulmamaktad�r. Do�rudan core projesidir. Bizler burdaki sevis i�erisinde MVC projesi oldu�unu g�steren servisi �a��rd���m�zda bu proje art�k bir mvc projesi olarak �al��acakt�r. Bu y�zden ConfigureServices metodu i�erisinde services.AddMvc() isimli metodu tan�mlad�k.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(x=>x.EnableEndpointRouting=false);
        }

      
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //wwwroot klas�r�n� kullanabilmek i�in a�a��daki metodu tan�mlamal�sn�z.
            app.UseStaticFiles();

            app.UseRouting();
            //custom route tan�mlamak isterseniz a�a��daki metodu kullanabilirsiniz.
            app.UseMvc(rotues =>
            {

                rotues.MapRoute(
                    name: "ProductRotue",
                    template: "SelectedProduct/{ProductName}/{productId}",
                    defaults: new { controller = "Product", action = "GetProduct" }
                    );

                rotues.MapRoute(
                   name: "ProductCategory",
                   template: "SelectedProductCategory/{CategoryName}/{categoryId}",
                   defaults: new { controller = "Product", action = "GetProductsWithCategory" }
                   );

                rotues.MapRoute(
                    name: "Default",
                    template: "{Controller=Home}/{Action=Index}/{id?}"
                    );
            });

            //Default route tan�mlamak isterseniz a�a��daki metodu kullanabilirsiniz.
            //app.UseMvcWithDefaultRoute();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Merhaba D�nya!");
                });
            });
        }
    }
}

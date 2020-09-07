using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GCIT.Core.Models;
using GCIT.Core.Modules.Remates.Implementations;
using GCIT.Core.Modules.Remates.Interface;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;
using GCIT.Core.Repository;
using Microsoft.EntityFrameworkCore;
using GCIT.Core.Repository.Contracts;
using GCIT.Core.Repository.Repositories;
using Microsoft.AspNetCore.Http;

namespace GCIT.Remates.WebApi
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            var connection = Configuration.GetConnectionString("RematesContext");
            services.AddDbContext<RematesContext>
                (options => options.UseSqlServer(connection));
            services.AddEntityFrameworkSqlServer();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            

            services.AddScoped<DbContext, RematesContext>();
            services.AddScoped<ITransactionsRepository, TransactionsRepository>();
            services.AddScoped<IUserCredentialsRepository, UserCredentialsRepository>();
            services.AddScoped<IRematesService, RematesService>();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Remates WebAPI", Version = "v1" });
            });

            services.Configure<MvcOptions>(options =>
            {
                //mvc options
            });

            //services.AddTransient<WSA.WSAdaptadorSoap>(provider =>
            //{
            //    var client = new WSA.WSAdaptadorSoapClient(WSA.WSAdaptadorSoapClient.EndpointConfiguration.WSAdaptadorSoap);
            //    client.Endpoint.Address = new System.ServiceModel.EndpointAddress(Configuration["Services:TokenService"]);
            //    return client;
            //});
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            
            app.UseSwagger();
            
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/GCIT.Remates.WebApi/swagger/v1/swagger.json", "Remates WebAPI");
            });

            app.UseHttpsRedirection();
            app.UseMvc(

                //routes => { routes.MapRoute(name: "defaultV2", template: "api/{controller}/{action}"); routes.MapRoute(name: "default", template: "api/{controller}/{id}"); }
            );
            
        }
    }
}

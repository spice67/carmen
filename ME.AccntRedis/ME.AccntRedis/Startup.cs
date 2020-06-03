using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Core.Contracts;
using ME.AccntRedis.Contracts;
using ME.AccntRedis.Data;
using ME.Account.Web.Core.Business;
using ME.Account.Web.Core.Contracts;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ME.AccntRedis
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
            services.AddSingleton(typeof(IRedisContext), typeof(RedisContext));
            services.AddTransient(typeof(ICustomerRepository), typeof(CustomerRepository));
            services.AddTransient(typeof(ICustomerAccountRepository), typeof(CustomerAccountRepository));
            services.AddTransient(typeof(ITransactionRepository), typeof(TransactionRepository));
            services.AddTransient(typeof(ICustomerInfoService), typeof(CustomerInfoService));
            services.AddTransient(typeof(ITransactionInfoService), typeof(TransactionInfoService));

            services.AddControllers();

            services.AddSwaggerDocument(config =>
            {
                config.PostProcess = document =>
                {
                    document.Info.Version = "v1";
                    document.Info.Title = "Customer Account API";
                    document.Info.Description = "A simple api for account crediting";
                    document.Info.TermsOfService = "None";
                    document.Info.Contact = new NSwag.OpenApiContact
                    {
                        Name = "Spice67",
                        Email = "spice@coolbox.se",
                        Url = string.Empty
                    };
                };
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseOpenApi();
            app.UseSwaggerUi3();
        }
    }
}

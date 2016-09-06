using System;
using AspnetCorePostgres.Postgres;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using AspnetCorePostgres.Postgres.POCO;

namespace AspnetCorePostgres
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            const string envName = "VCAP_SERVICES";
            var settings = Environment.GetEnvironmentVariable(envName);
            var jSettings = JObject.Parse(settings);
            var postgresCreds = jSettings["postgres"][0]["credentials"];
            var username = postgresCreds["username"];
            var password = postgresCreds["password"];
            var host = postgresCreds["host"];
            var port = postgresCreds["port"];
            var database = postgresCreds["database"];

            var connectionString = $"User ID={username};Password={password};Server={host};Port={port};Database={database};Pooling=true;";
            services.AddDbContext<AuthorsContext>(
                opts => opts.UseNpgsql(connectionString)
            );
            services.AddDbContext<PartFamilyContext>(
                opts => opts.UseNpgsql(connectionString)
            );
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();
            app.UseStaticFiles();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        public static void Main(string[] args)
        {
            var config = new ConfigurationBuilder()
                .AddCommandLine(args)
                .Build();

            var host = new WebHostBuilder()
                        .UseKestrel()
                        .UseConfiguration(config)
                        .UseIISIntegration()
                        .UseStartup<Startup>()
                        .Build();
            host.Run();
        }
    }
}

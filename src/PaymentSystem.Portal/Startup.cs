using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using PaymentSystem.Application;
using PaymentSystem.Application.Handlers;
using PaymentSystem.Domain;
using PaymentSystem.Infrastructure;
using PaymentSystem.Infrastructure.Persistance;
using PaymentSystem.Infrastructure.Services;
using PaymentSystem.Portal.Options;
using PaymentSystem.Portal.Services;
using PaymentSystem.Portal.TempData;
using PaymentSystem.ReadModel;
using PaymentSystem.ReadModel.Projections;
using PaymentSystem.ReadModel.Services;
using Swashbuckle.AspNetCore.Swagger;

namespace PaymentSystem.Portal
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
            
            services.AddControllersWithViews()
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<Startup>());

            // In production, the React files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/build";
            });

            services.AddTransient<ISystemClock, SystemClock>();
            
            //Normally i would separate into other smaller extensions as UseEventStorePersistance  
            services.AddSingleton<IEventStore, EventStore>();
            services.AddTransient(typeof(IAggregateRepository<>), typeof(AggregateRepository<>));
            services.AddSingleton(typeof(IProjectionRepository<>), typeof(InMemoryProjectionRepo<>));
            services.AddTransient<IEventPublisher, MediatorEventPublisher>();
            services.AddMediatR(typeof(Startup), typeof(CommandHandler<,>), typeof(IProjection));
            services.AddSingleton<IEventQueue, EventQueue>()
                .AddHostedService<EventProcessor>();

            services.AddOptions()
                .AddOptions<PaymentSystemOptions>("Options");

            services.AddSwaggerGen(c =>  c.SwaggerDoc("portal" , new OpenApiInfo {Title = "My API", Version = "v1"}));
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            
            app.UseSwagger();

            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1"); });


            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseReactDevelopmentServer(npmScript: "start");
                }
            });
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
        }
    }
}
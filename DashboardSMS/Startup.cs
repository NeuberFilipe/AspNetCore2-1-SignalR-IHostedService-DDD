using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Hubs;
using DashboardSMS.Service;
using DashboardSMS.Domain.Interfaces.Infra.Data;
using DashboardSMS.Infra.Data;
using DashboardSMS.Application;
using DashboardSMS.Domain.Interfaces.Application;

namespace DashboardSMS
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
            services.AddMvc();
            services.AddSignalR();

            services.AddSingleton(s => new SMSContext(Configuration.GetSection("Data").GetValue<string>("SMSConnectionString")));

            services.AddSingleton<ISMSRepository, SMSRepository>();

            services.AddSingleton<ISMSApplication, SMSApplication>();

            #region [ + ] Service Background
            services.AddHostedService<TimedHostedService>();

            services.AddHostedService<ConsumeScopedServiceHostedService>();
            services.AddSingleton<IScopedProcessingService, ScopedProcessingService>();

            services.AddHostedService<QueuedHostedService>();
            services.AddSingleton<IBackgroundTaskQueue, BackgroundTaskQueue>();
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseSignalR(routes =>
            {
                routes.MapHub<MonitoramentoHub>("/chats");
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}

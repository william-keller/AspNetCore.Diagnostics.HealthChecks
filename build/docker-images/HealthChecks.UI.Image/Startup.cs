﻿using HealthChecks.UI.Image.Configuration;
using HealthChecks.UI.Image.PushService;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HealthChecks.UI.Image
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
            services
                .AddHealthChecksUI()
                .AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            if (bool.TryParse(Configuration[PushServiceKeys.Enabled], out bool enabled))
            {
                services.AddTransient<HealthChecksPushService>();
            }
        }
        public void Configure(IApplicationBuilder app)
        {
            app.UseRouting()
                .UseEndpoints(config =>
                {
                    config.MapHealthChecksUI(Configuration);
                    config.MapDefaultControllerRoute();
                });
        }
    }
}

using Microsoft.AspNetCore.HttpOverrides;
using NLog;
using nuYApityalsya.Extensions;

namespace nuYApityalsya;

public class Startup
{
    [Obsolete]
    public Startup(IConfiguration configuration)
{
         LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));
         Configuration = configuration;
}

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
        services.ConfigureCors();
        services.ConfigureIISIntegration();
        services.AddControllers();
        services.ConfigureLoggerService();
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
        }
        app.UseHttpsRedirection();
        app.UseHsts();
        app.UseStaticFiles();
        app.UseCors("CorsPolicy");
        app.UseForwardedHeaders(new ForwardedHeadersOptions
        {
            ForwardedHeaders = ForwardedHeaders.All
        });
        app.UseRouting();
        app.UseAuthorization();
        app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
    }
}
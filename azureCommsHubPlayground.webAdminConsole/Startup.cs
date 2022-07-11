using Microsoft.EntityFrameworkCore;
using azureCommsHubPlayground.persistance.database;
using azureCommsHubPlayground.webAdminConsole.Services;
using azureCommsHubPlayground.azureHubCommService;

namespace azureCommsHubPlayground.webAdminConsole
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Environment { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<azureMessageBusSettings>(Configuration.GetSection("azureMessageBusSettings"));

            var connectionString = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<dbContext>(options =>
                options.UseSqlServer(connectionString));
            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddRazorPages();
            services.AddServerSideBlazor();

            services.AddScoped<ISessionState, sessionState>();
            services.AddScoped<IAzureMessageBusDispatcher, azureMessageBusDispatcher>();
            services.AddScoped<IIpAddressCheckRequestHandlerService, ipAddressCheckRequestHandlerService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseForwardedHeaders();
            app.UseDeveloperExceptionPage();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}

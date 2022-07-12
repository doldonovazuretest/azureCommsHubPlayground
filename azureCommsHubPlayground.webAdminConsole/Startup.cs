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
            // map part of the appsettings.json related to Azure Hub Message Queue to be consumed as dependency by IAzureMessageBusSubscriber and IAzureMessageBusDispatcher
            services.Configure<azureMessageBusSettings>(Configuration.GetSection("azureMessageBusSettings"));

            var connectionString = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<dbContext>(options =>
                options.UseSqlServer(connectionString));
            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddRazorPages();
            services.AddServerSideBlazor();

            // service called per each user session and will include all parameters utilized to reflect session state
            services.AddScoped<ISessionState, sessionState>();

            // service called to send messages to Azure Hub Message Queue - a dependency used by IIpAddressCheckRequestHandlerService
            services.AddScoped<IAzureMessageBusDispatcher, azureMessageBusDispatcher>();

            // service used by Blazor component to send request for a IP address check
            services.AddScoped<IIpAddressCheckRequestHandlerService, ipAddressCheckRequestHandlerService>();

            // service handling arriving messages from Azure Hub Message Queue - this service will provide callbacks to service right below
            services.AddSingleton<IAzureIncomingBusMessageEventDispatcher, azureIncomingBusMessageEventDispatcher>();

            // service incapsulating implementation of Azure Hub Messages processor
            services.AddSingleton<IAzureMessageBusSubscriber, azureMessageBusSubscriber>();
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

            // grab an instance of IServiceProvider
            var _iServiceProfider = app.ApplicationServices;

            // grab an instance of incoming message handler
            var _incomingMessageDispatcher = _iServiceProfider.GetService<IAzureIncomingBusMessageEventDispatcher>();

            // register delegates of message handler with Azure Hub Messages processor
            _iServiceProfider.GetService<IAzureMessageBusSubscriber>()
                .register(_incomingMessageDispatcher.processMessage, _incomingMessageDispatcher.processError, _incomingMessageDispatcher.setGuid)
                .GetAwaiter()
                .GetResult();
        }
    }
}

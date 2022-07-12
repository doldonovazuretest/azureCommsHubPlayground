using azureCommsHubPlayground.azureHubCommService;
using azureCommsHubPlayground.persistance.database;
using azureCommsHubPlayground.persistance.unitsOfWork;
using azureCommsHubPlayground.backEndService.Services;
using Microsoft.EntityFrameworkCore;

namespace azureCommsHubPlayground.backEndService
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

            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            // i know this is a hard dependency - i might think about refactoring it later 
            // this is used ultimately by IAzureBusMessageLogger to persist Azure Hub Messages received and processed by the backend service IAzureMessageBusSubscriber
            services.AddSingleton(new unitOfWork(connectionString));

            services.AddTransient<IIpAddressLookUpService, ipAddressLookUpService>();

            // service used to persist info on processed messaged to the underlying database
            services.AddSingleton<IAzureBusMessageLogger, azureBusMessageLogger>();

            // service used to report processed messages to the report Azure Hub Message Queue
            services.AddSingleton<IAzureMessageBusDispatcher, azureMessageBusDispatcher>();

            // service subscribed to handle incoming messages - will provide callback to IAzureMessageBusSubscriber
            services.AddSingleton<IAzureMessagePayLoadProcessor, azureMessagePayLoadProcessor>();

            // Azure Hub Message Queue listener service 
            services.AddSingleton<IAzureMessageBusSubscriber, azureMessageBusSubscriber>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            var _iServiceProfider = app.ApplicationServices;

            // IAzureMessagePayLoadProcessor is an internal library abstraction of message payload handler - basically library decides internally 
            // how it wants to further handle Azure Hub messages

            var _payLoadProcessor = _iServiceProfider.GetService<IAzureMessagePayLoadProcessor>();

            _iServiceProfider.GetService<IAzureMessageBusSubscriber>()
                .register(_payLoadProcessor.processMessage, _payLoadProcessor.processError, _payLoadProcessor.setGuid)
                .GetAwaiter()
                .GetResult();
        }
    }
}

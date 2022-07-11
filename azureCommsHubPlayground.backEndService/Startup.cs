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
            services.Configure<azureMessageBusSettings>(Configuration.GetSection("azureMessageBusSettings"));

            var connectionString = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<dbContext>(options =>
                options.UseSqlServer(connectionString));
            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            services.AddSingleton(new unitOfWork(connectionString));

            services.AddTransient<IIpAddressLookUpService, ipAddressLookUpService>();

            services.AddSingleton<IAzureBusMessageLogger, azureBusMessageLogger>();
            services.AddSingleton<IAzureMessageBusDispatcher, azureMessageBusDispatcher>();
            services.AddSingleton<IAzureMessagePayLoadProcessor, azureMessagePayLoadProcessor>();
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

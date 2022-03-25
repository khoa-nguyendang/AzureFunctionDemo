using AzureFunctionDemo.Efcore;
using AzureFunctionDemo.Usecases;
using AzureFunctionDemo.Usecases.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureServices(services => {
        services.AddDbContext<TransactionDbContext>(options => {
            options.UseSqlServer(Environment.GetEnvironmentVariable("ConnectionString") ?? string.Empty);
        });
        services.AddTransient<ITransactionVerifier, TransactionVerifier>();
    })
    .Build();

host.Run();
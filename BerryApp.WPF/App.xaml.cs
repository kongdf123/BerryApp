using BerryApp.Biz.Machines;
using BerryApp.Domain.Entities;
using BerryApp.Infra.Devices;
using BerryApp.Infra.Persistence;
using BerryApp.Shared.Events;
using BerryApp.Shared.Services;
using BerryApp.WPF.ViewModels;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Configuration;
using System.Data;
using System.Windows;

namespace BerryApp.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private ServiceProvider serviceProvider;

        public App()
        {
            var services = new ServiceCollection();

            var environment = Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT")
                  ?? "Production";
            var config = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false)
                .AddJsonFile($"appsettings.{environment}.json", optional: true)
                .Build();

            var connStr = config.GetConnectionString("Default");
            services.AddSingleton<IAlarmRepository>(x=>new AlarmRepository(connStr));
            services.AddSingleton<AlarmService>();

            services.AddSingleton<EventBus>();
            services.AddSingleton<NavigationService>();

            // Register services and repositories
            services.AddSingleton<IMachineRepository, InMemoryMachineRepository>();
            services.AddSingleton<PlcService>();
            services.AddSingleton<IMachineService, MachineService>();

            // Register device and monitoring services
            services.AddSingleton<ModbusTcpClient>(sp => new ModbusTcpClient("192.168.0.10"));
            services.AddSingleton<PlcMonitoringService>();
            services.AddSingleton<MachineMonitoringService>();


            // ViewModels
            services.AddSingleton<DashboardViewModel>();
            services.AddSingleton<OrdersViewModel>();
            services.AddSingleton<AlarmsViewModel>();

            services.AddSingleton<MainViewModel>();
            services.AddSingleton<MainWindow>();

            serviceProvider = services.BuildServiceProvider();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            var mainWindow = serviceProvider.GetRequiredService<MainWindow>();
            mainWindow.DataContext = serviceProvider.GetRequiredService<MainViewModel>();
            mainWindow.Show();
        }

        //public static MachineViewModel Init()
        //{
        //    var repo = new InMemoryMachineRepository();

        //    var machine = new Machine(Guid.Parse("11111111-1111-1111-1111-111111111111"), "Machine A");
        //    machine.CloseSafetyDoor();
        //    repo.Seed(machine);

        //    //var handler = new StartMachineHandler(repo);

        //    var modbus = new ModbusTcpClient("192.168.0.10");
        //    var plcMonitor = new PlcMonitoringService(modbus);
        //    var monitorService = new MachineMonitoringService(plcMonitor);

        //    return new MachineViewModel(monitorService);
        //    //return new MachineViewModel(handler);
        //}
    }

}

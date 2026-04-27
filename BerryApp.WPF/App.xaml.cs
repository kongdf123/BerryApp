using BerryApp.Biz.Machines;
using BerryApp.Domain.Entities;
using BerryApp.Infra.Devices;
using BerryApp.Infra.Persistence;
using BerryApp.Shared.Events;
using BerryApp.Shared.Services;
using BerryApp.WPF.ViewModels;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
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

            // 🔥 Configure Serilog
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(config)
                .CreateLogger();

            services.AddLogging(builder =>
            {
                builder.ClearProviders();
                builder.AddSerilog();
            });
            SetupGlobalExceptionHandling();

            var connStr = config.GetConnectionString("Default");
            services.AddSingleton<IAlarmRepository>(x=>new AlarmRepository(connStr));
            services.AddSingleton<AlarmService>();

            services.AddSingleton<EventBus>();
            services.AddSingleton<NavigationService>();

            // Register services and repositories
            services.AddSingleton<IMachineRepository, InMemoryMachineRepository>();
            services.AddSingleton<PlcService>();
            services.AddSingleton<IMachineService, MachineService>(); 
            services.AddSingleton<IPlcClient, OpcUaClient>();

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
            //var mainWindow = serviceProvider.GetRequiredService<MainWindow>();
            //mainWindow.DataContext = serviceProvider.GetRequiredService<MainViewModel>();
            //mainWindow.Show();
            try
            {
                base.OnStartup(e);

                var mainWindow = serviceProvider.GetService<MainWindow>();
                mainWindow.DataContext = serviceProvider.GetService<MainViewModel>();
                mainWindow.Show();

                //var opc = (OpcUaClient)serviceProvider.GetRequiredService<IPlcClient>();

                //await opc.ConnectAsync("opc.tcp://localhost:4840");
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Application startup failed");

                MessageBox.Show(
                    "Application failed to start. Check logs.",
                    "Fatal Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);

                Shutdown();
            }
        }

        protected override void OnExit(ExitEventArgs e)
        {
            Log.CloseAndFlush();
            base.OnExit(e);
        }

        private void SetupGlobalExceptionHandling()
        {
            // UI thread exceptions
            this.DispatcherUnhandledException += (sender, e) =>
            {
                Log.Error(e.Exception, "Unhandled UI Exception");

                MessageBox.Show(
                    "Unexpected error occurred. Please check logs.",
                    "Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);

                e.Handled = true; // prevent app crash
            };

            // Non-UI thread exceptions
            AppDomain.CurrentDomain.UnhandledException += (sender, e) =>
            {
                var ex = e.ExceptionObject as Exception;

                Log.Fatal(ex, "Unhandled AppDomain Exception");
            };

            // Task exceptions (async)
            TaskScheduler.UnobservedTaskException += (sender, e) =>
            {
                Log.Error(e.Exception, "Unobserved Task Exception");

                e.SetObserved(); // prevent process crash
            };
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

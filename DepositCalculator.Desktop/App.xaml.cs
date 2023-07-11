using System.Windows;
using DepositCalculator.Desktop.ViewModels;
using DepositCalculator.Domain.Abstractions;
using DepositCalculator.Domain.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DepositCalculator.Desktop
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static IHost? AppHost { get; set; }

        public App()
        {
            AppHost = Host.CreateDefaultBuilder()
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddSingleton<ICalculatorService, CalculatorService>();
                    services.AddSingleton<MainWindowViewModel>();
                    services.AddSingleton<MainWindow>();
                })
                .Build();
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            await AppHost?.StartAsync()!;

            var startupWindow = AppHost.Services.GetRequiredService<MainWindow>();
            startupWindow.Show();
            
            base.OnStartup(e);
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            await AppHost?.StopAsync()!;
            
            base.OnExit(e);
        }
    }
}
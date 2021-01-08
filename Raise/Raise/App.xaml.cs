using Raise.Utils;
using Raise.ViewModels;
using Raise.Views;
using Xamarin.Forms;

namespace Raise
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();
            DependencyService.RegisterSingleton(new AppCustomConfigurationViewModel());

            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
#if DEBUG
            HotReloader.Current.Run(this, new HotReloader.Configuration
            {
                PreviewerDefaultMode = HotReloader.PreviewerMode.On
                //optionaly you may specify EXTENSION's IP (ExtensionIpAddress) 
                //in case your PC/laptop and device are in different subnets
                //e.g. Laptop - 10.10.102.16 AND Device - 10.10.9.12
                //ExtensionIpAddress = IPAddress.Parse("10.10.102.16") // use your PC/Laptop IP
            });
#endif
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}

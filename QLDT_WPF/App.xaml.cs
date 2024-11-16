using System.Configuration;
using System.Data;
using System.Windows;
using Syncfusion.Licensing;
using System.Configuration;

namespace QLDT_WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Thêm license key vào đây
            string licenseKey = ConfigurationManager.AppSettings["SyncfusionLicenseKey"];
            SyncfusionLicenseProvider.RegisterLicense(licenseKey);
        }
    }

}

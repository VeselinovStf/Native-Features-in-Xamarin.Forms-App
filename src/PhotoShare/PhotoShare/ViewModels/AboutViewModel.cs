using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace PhotoShare.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        public string AppName { get => AppInfo.Name;  }
        public string AppVersion { get => AppInfo.VersionString; }

        public AboutViewModel()
        {
            Title = "About";
            OpenWebCommand = new Command(async () => await Browser.OpenAsync("https://aka.ms/xamain-quickstart"));
        }

        public ICommand OpenWebCommand { get; }

        public string Platform
        {
            get
            {
                switch (Device.RuntimePlatform)
                {
                    case Device.iOS:
                        if (Device.Idiom == TargetIdiom.Tablet)
                        {
                            return "iPad";
                        }
                        else
                        {
                            return "iPhone";
                        }
                    default:
                        return Device.RuntimePlatform;
                }
            }
        }
    }
}
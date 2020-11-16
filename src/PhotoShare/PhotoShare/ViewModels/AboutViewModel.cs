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
    }
}
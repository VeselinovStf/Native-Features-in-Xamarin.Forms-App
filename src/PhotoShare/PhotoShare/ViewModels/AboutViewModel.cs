using System;
using System.Threading;
using System.Timers;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace PhotoShare.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        public string AppName { get => AppInfo.Name; }
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

        private string _countDownLabel = "Countdown not triggered";

        public string CountDownLabel
        {
            get { return _countDownLabel; }
            set { SetProperty(ref _countDownLabel, value); } 
        }

        private string _countDownText = "";
        public string CountDownText
        {
            get { return _countDownText; }
            set
            {
                SetProperty(ref _countDownText, value);
                if (value != "")
                {
                    CountDownLabel = "Countdown triggered";
                    ResetTimer();
                }
            }
        }

        private System.Threading.Timer timer;
        private void ResetTimer()
        {
            if (timer == null)
            {
                timer = new System.Threading.Timer(
                    new TimerCallback(TextTime),
                    null,
                    2000,
                    Timeout.Infinite
                    );
            }
            else
            {
                timer.Change(Timeout.Infinite, Timeout.Infinite);
                timer.Change(2000, Timeout.Infinite);

            }
        }

        private void TextTime(object state)
        {
            CountDownLabel = "CountDown COMPLETE";
            MessagingCenter.Send<AboutViewModel>(this, "HideKeyboard");
            timer.Change(Timeout.Infinite, Timeout.Infinite);
        }
    }

   
}

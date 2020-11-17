using System;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace PhotoShare.ViewModels
{
    public class PictureViewModel : BaseViewModel
    {
        private bool _EnableShareButton = false;

        public Command PickPhotoCommand { get; set; }

        public Command ShareCommand { get; set; }

        public Command SettingsCommand { get; set; }

        public PictureViewModel()
        {
            Title = "Pictures";

            int i = 0;
            Device.StartTimer(new TimeSpan(0, 0, 1), () =>
              {
                  if (i > 1000)
                  {
                      KeepGoing = false;
                  }

                  Device.BeginInvokeOnMainThread(() =>
                  {
                      BoxOpacity = i / 100.0;
                  });

                  i += 5;

                  return KeepGoing;
              });

            PickPhotoCommand = new Command(async () => await PickPhoto());
            ShareCommand = new Command(async () => await SharePhoto(), () => _EnableShareButton);
            SettingsCommand = new Command(() => AppInfo.ShowSettingsUI());
        }

        public void StopFade()
        {
            if (KeepGoing)
            {
                KeepGoing = false;
                BoxOpacity = 1.0;
            }
        }

        private async Task SharePhoto()
        {
            throw new NotImplementedException();
        }

        private async Task PickPhoto()
        {
            StopFade();

            var status = await Permissions.CheckStatusAsync<Permissions.Photos>();

            if (status == PermissionStatus.Unknown)
            {
                status = await Permissions.RequestAsync<Permissions.Photos>();
            }

            if (status ==  PermissionStatus.Denied)
            {
                ShowFixSettings = true;

                await App.Current.MainPage.DisplayAlert(Title,
                    "Photo Pick Denied - fix in settings",
                    "Ok");
            }
            else
            {
                ShowFixSettings = false;
            }
        }

        private string _buttonLabel = "Pick Picture";

        private double _boxOpacity;
        public double BoxOpacity
        {
            get { return _boxOpacity; }
            set { SetProperty(ref _boxOpacity, value); }
        }

        public bool KeepGoing { get; set; } = true;

        public string ButtonLabel
        {
            get { return _buttonLabel; }
            set { SetProperty(ref _buttonLabel, value); }
        }

        private ImageSource _imageSource = null;

        public ImageSource ImageSource
        {
            get { return _imageSource; }
            set { SetProperty(ref _imageSource, value); }
        }

        private bool _showFixSettings = false;

        public bool ShowFixSettings
        {
            get { return _showFixSettings; }
            set { SetProperty(ref _showFixSettings, value); }
        }

    }
}

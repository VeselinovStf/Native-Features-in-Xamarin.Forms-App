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

            PickPhotoCommand = new Command(async () => await PickPhoto());
            ShareCommand = new Command(async () => await SharePhoto(), () => _EnableShareButton);
            SettingsCommand = new Command(() => AppInfo.ShowSettingsUI());
        }

        private async Task SharePhoto()
        {
            throw new NotImplementedException();
        }

        private async Task PickPhoto()
        {
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

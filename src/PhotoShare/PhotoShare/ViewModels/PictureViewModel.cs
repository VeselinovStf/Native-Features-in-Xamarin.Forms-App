using PhotoShare.Services;
using System;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Microsoft.Extensions.DependencyInjection;

namespace PhotoShare.ViewModels
{
    public class PictureViewModel : BaseViewModel
    {
        //Header let us know - png / jpeg
        private static readonly byte[] PngHeader = new byte[] { 137, 80, 78, 71, 13, 10, 26, 10 };
        private byte[] cachedImage = null;

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
            var fn = IsPng() ? "attachment.png" : "attachment.jpg";

            var shareFileName = Path.Combine(FileSystem.CacheDirectory, fn);

            File.WriteAllBytes(shareFileName, cachedImage);

            await Share.RequestAsync(new ShareFileRequest
            {
                Title = "Shared from me",
                File = new ShareFile(shareFileName),
                PresentationSourceBounds = DeviceInfo.Platform == DevicePlatform.iOS && DeviceInfo.Idiom == DeviceIdiom.Tablet
                    ? new System.Drawing.Rectangle(0, 20, 0, 0)
                    : System.Drawing.Rectangle.Empty
            });
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

                //var sharedPhoto = await DependencyService.Get<IPhotoPickerService>()
                //                          .GetImageStreamAsync();
                var sharedPhotoService =  App.ServiceProvider.GetService<IPhotoPickerService>();
                var sharedPhoto = await sharedPhotoService.GetImageStreamAsync();                  


                if (sharedPhoto != null)
                {
                    BoxOpacity = 0;

                    // Make a copy of the image stream as ImageSource.FromStream() will 
                    // close the source

                    var stream = new MemoryStream();
                    sharedPhoto.ImageData.CopyTo(stream);
                    cachedImage = stream.ToArray();

                    ImageSource = ImageSource.FromStream(() => new MemoryStream(cachedImage));

                    ButtonLabel = "Pick another picture";
                    _EnableShareButton = true;
                    ShareCommand.ChangeCanExecute();
                }
            }
        }

        private bool IsPng()
        {
            bool result;
            int i = 0;

            do
            {
                result = cachedImage[i] == PngHeader[i];
            } while (result && ++i < 8);

            return result;
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

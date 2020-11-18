
using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;
using Plugin.CurrentActivity;
using System.Threading.Tasks;
using Android.Content;
using PhotoShare.Services.Models;
using PhotoShare.Services;
using Microsoft.Extensions.DependencyInjection;

namespace PhotoShare.Droid
{
    [Activity(Label = "PhotoShare", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        public static readonly int PickImageId = 1000;

        public TaskCompletionSource<SharePhoto> PickImageTaskCompletionSource { get; set; }

        internal static MainActivity Instance { get; private set; }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            Instance = this;

            CrossCurrentActivity.Current.Init(this, savedInstanceState);

            global::Xamarin.Forms.Forms.SetFlags("CollectionView_Experimental");
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);

            StartUp.Init(ConfigureServices);

            LoadApplication(new App());
        }

        void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IToastMessage, ToastMessage>();
            services.AddTransient<IKeyboardHelper, KeyBoardHelper>();
            services.AddTransient<IPhotoPickerService, PhotoPickerService>();
        }


        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);

            if (requestCode == PickImageId)
            {
                if ((resultCode == Result.Ok) && (data != null))
                {
                    SharePhoto sharedPhoto = new SharePhoto
                    {
                        ImageName = data.Data.ToString(),
                        ImageData = ContentResolver.OpenInputStream(data.Data)
                    };

                    PickImageTaskCompletionSource.SetResult(sharedPhoto);
                }
                else
                {
                    PickImageTaskCompletionSource.SetResult(null);
                }
            }
        }
    }
}
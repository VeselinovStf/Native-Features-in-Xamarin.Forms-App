using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using PhotoShare.Services;
using PhotoShare.Services.Models;
using Xamarin.Forms;

//[assembly: Dependency(typeof(PhotoShare.Droid.PhotoPickerService))]
namespace PhotoShare.Droid
{
    public class PhotoPickerService : IPhotoPickerService
    {
        public Task<SharePhoto> GetImageStreamAsync()
        {
            Intent intent = new Intent();

            intent.SetType("image/*");
            intent.SetAction(Intent.ActionGetContent);

            MainActivity.Instance.StartActivityForResult(
                Intent.CreateChooser(intent, "Select Photo"),
                MainActivity.PickImageId);

            MainActivity.Instance.PickImageTaskCompletionSource = new TaskCompletionSource<SharePhoto>();

            return MainActivity.Instance.PickImageTaskCompletionSource.Task;
        }
    }
}
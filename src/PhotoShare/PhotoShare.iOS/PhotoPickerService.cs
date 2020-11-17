using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;
using PhotoShare.Services;
using Xamarin.Forms;
using System.Threading.Tasks;
using PhotoShare.Services.Models;

[assembly: Dependency(typeof(PhotoShare.iOS.PhotoPickerService))]
namespace PhotoShare.iOS
{
    public class PhotoPickerService : IPhotoPickerService
    {
        TaskCompletionSource<SharePhoto> taskCompletionSource;

        UIImagePickerController imagePicker;

        public Task<SharePhoto> GetImageStreamAsync()
        {
            imagePicker = new UIImagePickerController
            {
                SourceType = UIImagePickerControllerSourceType.PhotoLibrary,
                MediaTypes = UIImagePickerController.AvailableMediaTypes(UIImagePickerControllerSourceType.PhotoLibrary)
            };

            imagePicker.FinishedPickingMedia += OnImagePickerFinishedPickingMedia;
            imagePicker.Canceled += OnImagePickerCancelled;

            UIWindow window = UIApplication.SharedApplication.KeyWindow;
            var viewController = window.RootViewController;
            viewController.PresentModalViewController(imagePicker, true);

            taskCompletionSource = new TaskCompletionSource<SharePhoto>();

            return taskCompletionSource.Task;
        }

        private void OnImagePickerFinishedPickingMedia(object sender, UIImagePickerMediaPickedEventArgs e)
        {
            UIImage image = e.EditedImage ?? e.OriginalImage;

            if (image != null)
            {
                NSData data;

                if (e.ReferenceUrl.PathExtension.ToUpper().Equals("PNG"))
                {
                    data = image.AsPNG();
                }
                else
                {
                    data = image.AsJPEG(1);
                }

                SharePhoto sharedPhoto = new SharePhoto
                {
                    ImageName = e.ReferenceUrl.ToString(),
                    ImageData = data.AsStream()
                };

                UnregisterEventHandlers();

                taskCompletionSource.SetResult(sharedPhoto);
            }
            else
            {
                UnregisterEventHandlers();
                taskCompletionSource.SetResult(null);
            }
            imagePicker.DismissModalViewController(true);
        }

        void OnImagePickerCancelled(object sender, EventArgs args)
        {
            UnregisterEventHandlers();
            taskCompletionSource.SetResult(null);
            imagePicker.DismissModalViewController(true);
        }

        void UnregisterEventHandlers()
        {
            imagePicker.FinishedPickingMedia -= OnImagePickerFinishedPickingMedia;
            imagePicker.Canceled -= OnImagePickerCancelled;
        }
    }
}
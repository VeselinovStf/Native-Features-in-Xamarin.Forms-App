using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using PhotoShare.Services;
using UIKit;
using CoreFoundation;

namespace PhotoShare.iOS
{
    public class ToastMessage : IToastMessage
    {
        public void OpenToast(string text)
        {
            var rootViewController = UIApplication.SharedApplication.KeyWindow.RootViewController;

            var alertController = UIAlertController.Create(string.Empty,
                text,
                UIAlertControllerStyle.Alert);

            alertController.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Default, null));

            rootViewController.PresentViewController(alertController, true, () =>
            {
                var delayTime = new DispatchTime(DispatchTime.Now, 3000000000);
                DispatchQueue.MainQueue.DispatchAlert(delayTime, () =>
                {
                    alertController.DismissViewController(true, null);
                });
            });
        }
    }
}
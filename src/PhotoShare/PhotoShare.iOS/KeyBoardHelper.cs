using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using PhotoShare.Services;
using UIKit;

[assembly: Xamarin.Forms.Dependency(typeof(PhotoShare.iOS.KeyBoardHelper))]
namespace PhotoShare.iOS
{
    public class KeyBoardHelper : IKeyboardHelper
    {
        public void HideKeyboard()
        {
            UIApplication.SharedApplication.KeyWindow.EndEditing(true);
        }
    }
}
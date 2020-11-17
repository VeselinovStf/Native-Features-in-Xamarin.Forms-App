using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Views.InputMethods;
using Android.Widget;
using PhotoShare.Services;
using Plugin.CurrentActivity;

[assembly: Xamarin.Forms.Dependency(typeof(PhotoShare.Droid.KeyBoardHelper))]
namespace PhotoShare.Droid
{

    public class KeyBoardHelper : IKeyboardHelper
    {
        public void HideKeyboard()
        {
            var inputMethodManager = InputMethodManager
                .FromContext(
                    CrossCurrentActivity.Current.Activity.ApplicationContext
                    );

            if (inputMethodManager != null)
            {
                inputMethodManager
                    .HideSoftInputFromWindow(
                        CrossCurrentActivity.Current.Activity.Window.DecorView.WindowToken,
                        HideSoftInputFlags.None
                        );
            }
        }
    }
}
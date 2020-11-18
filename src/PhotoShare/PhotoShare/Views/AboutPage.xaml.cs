using PhotoShare.Services;
using PhotoShare.ViewModels;
using Xamarin.Forms;
using Microsoft.Extensions.DependencyInjection;

namespace PhotoShare.Views
{
    public partial class AboutPage : ContentPage
    {
        public AboutPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            MessagingCenter.Subscribe<AboutViewModel>(this, "HideKeyboard", (sender) =>
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    App.ServiceProvider.GetService<IKeyboardHelper>().HideKeyboard();
                    App.ServiceProvider.GetService<IToastMessage>().OpenToast("Goodbye keyboard"); ;                                     
                });
            });
        }
    }
}
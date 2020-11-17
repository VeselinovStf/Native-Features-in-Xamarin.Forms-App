using PhotoShare.Services;
using PhotoShare.ViewModels;
using Xamarin.Forms;

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
                    DependencyService.Get<IKeyboardHelper>()?.HideKeyboard();
                });
            });
        }
    }
}
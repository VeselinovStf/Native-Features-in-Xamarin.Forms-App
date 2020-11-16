using System;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace PhotoShare
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        NetworkAccess LastNetworkAccess = NetworkAccess.Unknown;

        public AppShell()
        {
            InitializeComponent();

            Connectivity.ConnectivityChanged += Conectivity_Changed;
        }

        private async void Conectivity_Changed(object sender, ConnectivityChangedEventArgs e)
        {
            var access = e.NetworkAccess;

            if (access != LastNetworkAccess)
            {
                LastNetworkAccess = access;

                if (access == NetworkAccess.Internet)
                {
                    await App.Current.MainPage.DisplayAlert("Network Access", "Internet access availible", "Ok");
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Network Access", "Internet access unavailible", "Ok");
                }
            }
        }

        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//LoginPage");
        }
    }
}

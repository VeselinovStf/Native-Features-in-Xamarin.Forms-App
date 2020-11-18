

using Microsoft.Extensions.DependencyInjection;
using System;
using Xamarin.Forms;

namespace PhotoShare
{
    public partial class App : Application
    {
        public static IServiceProvider ServiceProvider { get; set; }
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
        }



        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}

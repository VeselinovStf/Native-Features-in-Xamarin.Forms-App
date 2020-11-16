using System.ComponentModel;
using Xamarin.Forms;
using PhotoShare.ViewModels;

namespace PhotoShare.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}
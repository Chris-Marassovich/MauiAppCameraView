using MauiAppCameraView.Models;
using MauiAppCameraView.PageModels;

namespace MauiAppCameraView.Pages
{
    public partial class MainPage : ContentPage
    {
        public MainPage(MainPageModel model)
        {
            InitializeComponent();
            BindingContext = model;
        }
    }
}
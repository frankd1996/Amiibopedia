using Amiibopedia.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Amiibopedia
{
    public partial class MainPage : ContentPage
    {
        public MainPageViewModel ViewModel { get; set; }
        public MainPage()
        {
            InitializeComponent();
        }

        //TODO: Antipatrón de programación asíncrona. Revisar
        //Cuando se sobreescribe, OnAppearing permite personalizar el comportamiento 
        // antes de que Xamarin.Forms.Page se vuelva visible.
        protected override void OnAppearing()
        {
            ViewModel = new MainPageViewModel();
            Task.Run(async () => await ViewModel.LoadCharacters()).Wait();
            this.BindingContext = ViewModel;          
            base.OnAppearing();            
        }

        private async void ViewCell_Appearing(object sender, EventArgs e)
        {
            var cell = sender as ViewCell;
            var view = cell.View;
            view.TranslationX = -100;
            view.Opacity = 0;

            await Task.WhenAny<bool>
                (
                view.TranslateTo(0, 0, 250, Easing.SinIn),
                view.FadeTo(1,500,Easing.BounceIn)
                );            
        }
    }
}

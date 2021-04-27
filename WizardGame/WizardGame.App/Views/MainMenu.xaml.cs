using System;

using Windows.UI.Xaml.Controls;
using WizardGame.App.Services;
using WizardGame.App.ViewModels;

namespace WizardGame.App.Views
{
    public sealed partial class MainMenu : Page
    {
        public BlankViewModel ViewModel { get; } = new BlankViewModel();

        public MainMenu()
        {
            InitializeComponent();
        }

        private void Image_GettingFocus(Windows.UI.Xaml.UIElement sender, Windows.UI.Xaml.Input.GettingFocusEventArgs args)
        {

        }

        private void OnClickToStartGame(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            NavigationService.Navigate(typeof(MainPage));
        }
    }
}

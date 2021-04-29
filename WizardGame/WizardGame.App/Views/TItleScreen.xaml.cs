using System;

using Windows.UI.Xaml.Controls;
using WizardGame.App.Services;
using WizardGame.App.ViewModels;

namespace WizardGame.App.Views
{
    public sealed partial class TitleScreen : Page
    {

        public TitleScreen()
        {
            InitializeComponent();
        }

        private void OnClickToStartGame(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            NavigationService.Navigate<Game>();
        }

        private void OnClickToContinueGame(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            //NavigationService.Navigate<TabViewPage>();
        }

        private void OnClickToEnterSpellBook(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            NavigationService.Navigate<SpellBookPage>();
        }

        private void OnClickToEnterSettings(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            NavigationService.Navigate<SettingsPage>();
        }

        private void OnClickToQuitGame(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            Windows.UI.Xaml.Application.Current.Exit();
        }
    }
}

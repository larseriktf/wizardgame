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

        private void OnStartGame(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            NavigationService.Navigate<GamePage>();
        }

        private void OnOpenSpellBook(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            TitleScreenFrame.Navigate(typeof(SpellBookPage));
        }

        private void OnOpenLeaderboards(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            TitleScreenFrame.Navigate(typeof(LeaderboardsPage));
        }

        private void OnOpenSettings(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            TitleScreenFrame.Navigate(typeof(SettingsPage));
        }

        private void OnQuitGame(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            Windows.UI.Xaml.Application.Current.Exit();
        }
    }
}

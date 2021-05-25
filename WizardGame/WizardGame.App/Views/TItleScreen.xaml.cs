using System;

using Windows.UI.Xaml.Controls;
using WizardGame.App.Services;
using WizardGame.App.ViewModels;
using WizardGame.Model;

namespace WizardGame.App.Views
{
    public sealed partial class TitleScreen : Page
    {
        public PlayerProfileViewModel ProfileViewModel { get; } = new PlayerProfileViewModel();

        public TitleScreen()
        {
            DataContext = this;

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

        private void OnOpenPlayerProfile(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            TitleScreenFrame.Navigate(typeof(PlayerProfilePage));
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

        private async void OnLoadedAsync(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            await ProfileViewModel.LoadSelectedPlayerAsync();
        }
    }
}

using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using WizardGame.App.Services;
using WizardGame.App.ViewModels;
using WizardGame.Model;

namespace WizardGame.App.Views
{
    public sealed partial class TitleScreen : Page
    {
        public PlayerProfileViewModel ViewModel { get; } = new PlayerProfileViewModel();

        public TitleScreen()
        {
            DataContext = this;

            InitializeComponent();
        }

        private void OnStartGame(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate<GamePage>(ViewModel.SelectedPlayer);
        }

        private void OnOpenSpellBook(object sender, RoutedEventArgs e)
        {
            TitleScreenFrame.Navigate(typeof(SpellBookPage));
        }

        private void OnOpenPlayerProfile(object sender, RoutedEventArgs e)
        {
            TitleScreenFrame.Navigate(typeof(PlayerProfilePage));
        }

        private void OnOpenLeaderboards(object sender, RoutedEventArgs e)
        {
            TitleScreenFrame.Navigate(typeof(LeaderboardsPage));
        }

        private void OnOpenSettings(object sender, RoutedEventArgs e)
        {
            TitleScreenFrame.Navigate(typeof(SettingsPage));
        }

        private void OnToggleExitWindow(object sender, RoutedEventArgs e)
        {
            if (ComfirmExitGrid.Visibility == Visibility.Visible)
            {
                ComfirmExitGrid.Visibility = Visibility.Collapsed;
            }
            else
            {
                ComfirmExitGrid.Visibility = Visibility.Visible;
            }
        }

        private async void OnLoadedAsync(object sender, RoutedEventArgs e)
        {
            await ViewModel.LoadSelectedPlayerAsync();

            if (ViewModel.SelectedPlayer != null)
            {
                SelectedPlayerProgressRing.Visibility = Visibility.Collapsed;
                SelectedPlayerStackPanel.Visibility = Visibility.Visible;
                SelectedPlayerNameTextBlock.Text = ViewModel.SelectedPlayer.PlayerName;
            }
        }

        private void OnComfirmExit(object sender, RoutedEventArgs e)
        {
            Application.Current.Exit();
        }
    }
}

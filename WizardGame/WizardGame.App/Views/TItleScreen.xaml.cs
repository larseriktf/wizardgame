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
            PlayerProfileViewModel.SelectedPlayerChangedEvent += OnSelectedPlayerChangedEventAsync;
            InitializeComponent();
        }

        public async void OnSelectedPlayerChangedEventAsync(object sender, EventArgs e)
        {
            await ViewModel.LoadSelectedPlayerAsync();
            ViewModel.SelectedPlayer = (sender as PlayerProfilePage).ViewModel.SelectedPlayer;
        }

        private void OnStartGame(object sender, RoutedEventArgs e) =>
            NavigationService.Navigate<GamePage>(ViewModel.SelectedPlayer);

        private void OnOpenSpellBook(object sender, RoutedEventArgs e) =>
            TitleScreenFrame.Navigate(typeof(SpellBookPage));

        private void OnOpenPlayerProfile(object sender, RoutedEventArgs e) =>
            TitleScreenFrame.Navigate(typeof(PlayerProfilePage));

        private void OnOpenLeaderboards(object sender, RoutedEventArgs e) =>
            TitleScreenFrame.Navigate(typeof(LeaderboardsPage), ViewModel.SelectedPlayer);

        private void OnOpenSettings(object sender, RoutedEventArgs e) =>
            TitleScreenFrame.Navigate(typeof(SettingsPage));

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

            SelectedPlayerProgressRing.Visibility = Visibility.Collapsed;
            SelectedPlayerContentControl.Visibility = Visibility.Visible;

            StartGameButton.IsEnabled = true;
            LeaderboardsButton.IsEnabled = true;
        }

        private void OnComfirmExit(object sender, RoutedEventArgs e) => Application.Current.Exit();
    }
}

using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using WizardGame.App.ViewModels;
using WizardGame.Model;

namespace WizardGame.App.Views
{
    public sealed partial class LeaderboardsPage : Page
    {
        public GameStatisticViewModel ViewModel { get; } = new GameStatisticViewModel();
        public PlayerProfile SelectedPlayer { get; set; }

        public LeaderboardsPage()
        {
            DataContext = this;
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            try
            {
                SelectedPlayer = e.Parameter as PlayerProfile;
            }
            catch (Exception exception)
            {
                SelectedPlayer = new PlayerProfile()
                {
                    Id = 0,
                    PlayerName = "Undefined",
                    IsSelected = true,
                    GameStatistics = null
                };
                Console.WriteLine(exception.StackTrace);
            }
        }

        private async void OnLoadedAsync(object sender, RoutedEventArgs e)
        {
            await ViewModel.LoadPlayerGamesAsync(SelectedPlayer.Id);
            await ViewModel.LoadTopGamesAsync();
        }

    }
}

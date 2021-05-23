using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using WizardGame.App.ViewModels;

namespace WizardGame.App.Views
{
    public sealed partial class PlayerProfilePage : Page
    {
        public PlayerProfilePageViewModel PlayerProfileViewModel { get; } = new PlayerProfilePageViewModel();

        public PlayerProfilePage()
        {
            InitializeComponent();
        }

        private async void OnLoadedAsync(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            await PlayerProfileViewModel.LoadAllPlayerProfilesAsync();
        }

        private void OnToggleProfileCreationMenu(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            if (ProfileCreationMenu.Visibility == Visibility.Visible)
            {
                ProfileCreationMenu.Visibility = Visibility.Collapsed;
            }
            else
            {
                ProfileCreationMenu.Visibility = Visibility.Visible;
            }
        }

        private async void OnAddPlayerProfileAsync(object sender, RoutedEventArgs e)
        {
            OnToggleProfileCreationMenu(sender, e);
            await PlayerProfileViewModel.AddNewPlayerProfileAsync("heyo");
        }
    }
}

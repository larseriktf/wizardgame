using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using WizardGame.App.ViewModels;
using WizardGame.Model;

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


        private async void OnAddPlayerProfileAsync(object sender, RoutedEventArgs e)
        {
            // Get objects
            Button btn = sender as Button;
            TextBox textBox = btn.Tag as TextBox;

            // Adds
            await PlayerProfileViewModel.AddNewPlayerProfileAsync(textBox.Text);

            // Clear textBox
            textBox.Text = string.Empty;
        }

        //private void OnClickPlayerProfile(object sender, ItemClickEventArgs e)
        //{
            
        //    PlayerNameTextBlock.Text = ;
        //    WaveNumberTextBlock.Text = "20";
        //}

        private void OnCLickPlayerProfile(object sender, RoutedEventArgs e)
        {
            PlayerProfile profile = (sender as Button).DataContext as PlayerProfile;

            PlayerIdTextBlock.Text = profile.Id.ToString();
            PlayerNameTextBlock.Text = profile.PlayerName;
            WaveNumberTextBlock.Text = "Infinite";
        }

        private async void OnEditProfileAsync(object sender, RoutedEventArgs e)
        {
            if (PlayerIdTextBlock.Text.Length == 0)
            {
                return;
            }
        }

        private async void OnDeleteProfileAsync(object sender, RoutedEventArgs e)
        {
            string Id = PlayerIdTextBlock.Text;

            if (Id.Length == 0)
            {
                return;
            }

            await PlayerProfileViewModel.DeletePlayerProfileAsync(Int32.Parse(Id));

            ClearProfileTextBlocks();
        }

        private void ClearProfileTextBlocks()
        {
            PlayerIdTextBlock.Text = string.Empty;
            PlayerNameTextBlock.Text = string.Empty;
            WaveNumberTextBlock.Text = string.Empty;
        }
    }
}

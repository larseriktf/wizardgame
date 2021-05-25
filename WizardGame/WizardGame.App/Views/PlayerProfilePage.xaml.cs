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

            // Updates the grid view
            await PlayerProfileViewModel.LoadAllPlayerProfilesAsync();

            // Clear textBox
            textBox.Text = string.Empty;
        }

        //private void OnCLickPlayerProfile(object sender, RoutedEventArgs e)
        //{
        //    PlayerProfile profile = (sender as Button).DataContext as PlayerProfile;

        //    PlayerIdTextBlock.Text = profile.Id.ToString();
        //    PlayerNameTextBlock.Text = profile.PlayerName;
        //    WaveNumberTextBlock.Text = "Infinite";

        //    EditProfileButton.IsEnabled = true;
        //    DeleteProfileButton.IsEnabled = true;
        //}

        private async void OnDeleteProfileAsync(object sender, RoutedEventArgs e)
        {
            string Id = PlayerIdTextBlock.Text;

            if (Id.Length == 0)
            {
                return;
            }

            await PlayerProfileViewModel.DeletePlayerProfileAsync(Int32.Parse(Id));

            // Updates the grid view
            await PlayerProfileViewModel.LoadAllPlayerProfilesAsync();

            ClearProfileTextBlocks();
        }

        private void ClearProfileTextBlocks()
        {
            PlayerIdTextBlock.Text = string.Empty;
            PlayerNameTextBlock.Text = string.Empty;
            WaveNumberTextBlock.Text = string.Empty;
        }

        private async void OnApplyEditProfileAsync(object sender, RoutedEventArgs e)
        {
            string Id = PlayerIdTextBlock.Text;
            string newName = UpdatedPlayerNameTextBox.Text;

            if (Id.Length == 0)
            {
                return;
            }

            await PlayerProfileViewModel.UpdatePlayerProfileAsync(Int32.Parse(Id), newName);

            // Updates the grid view
            await PlayerProfileViewModel.LoadAllPlayerProfilesAsync();

            UpdatedPlayerNameTextBox.Text = string.Empty;

            OnToggleProfileEdit(sender, e);
        }

        private void OnToggleProfileEdit(object sender, RoutedEventArgs e)
        {
            if (PlayerInfoPanel.Visibility == Visibility.Visible)
            {
                PlayerInfoPanel.Visibility = Visibility.Collapsed;
                EditPlayerPanel.Visibility = Visibility.Visible;
            }
            else
            {
                PlayerInfoPanel.Visibility = Visibility.Visible;
                EditPlayerPanel.Visibility = Visibility.Collapsed;
            }
        }

        private void OnCLickPlayerProfile(object sender, ItemClickEventArgs e)
        {
            PlayerProfile profile = e.ClickedItem as PlayerProfile;

            PlayerIdTextBlock.Text = profile.Id.ToString();
            PlayerNameTextBlock.Text = profile.PlayerName;
            WaveNumberTextBlock.Text = "Infinite";

            EditProfileButton.IsEnabled = true;
            DeleteProfileButton.IsEnabled = true;
        }
    }
}

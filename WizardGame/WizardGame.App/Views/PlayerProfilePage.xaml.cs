using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using WizardGame.App.ViewModels;
using WizardGame.Model;

namespace WizardGame.App.Views
{
    public sealed partial class PlayerProfilePage : Page
    {
        public delegate void SelectedPlayerChangedEventHandler(object sender, EventArgs e);
        public PlayerProfileViewModel ViewModel { get; } = new PlayerProfileViewModel();

        public PlayerProfilePage()
        {
            DataContext = this;
            InitializeComponent();
        }

        private async void OnLoadedAsync(object sender, RoutedEventArgs e)
        {
            await ViewModel.LoadAllPlayerProfilesAsync();

            PlayerProfilesProgressRing.Visibility = Visibility.Collapsed;
        }

        private async void OnAddPlayerProfileAsync(object sender, RoutedEventArgs e)
        {
            // Get objects
            Button btn = sender as Button;
            TextBox textBox = btn.Tag as TextBox;

            // Adds
            await ViewModel.AddNewPlayerProfileAsync(textBox.Text);

            // Clear textBox
            textBox.Text = string.Empty;
        }


        private async void OnDeleteProfileAsync(object sender, RoutedEventArgs e)
        {
            string Id = PlayerIdTextBlock.Text;

            if (Id.Length == 0)
            {
                return;
            }

            await ViewModel.DeletePlayerProfileAsync(Int32.Parse(Id));


            ClearProfileTextBlocks();
        }

        private void ClearProfileTextBlocks()
        {
            PlayerIdTextBlock.Text = string.Empty;
            PlayerNameTextBlock.Text = string.Empty;
        }

        private async void OnApplyEditProfileAsync(object sender, RoutedEventArgs e)
        {
            string Id = PlayerIdTextBlock.Text;
            string newName = UpdatedPlayerNameTextBox.Text;

            if (Id.Length == 0)
            {
                return;
            }

            await ViewModel.UpdatePlayerProfileAsync(Int32.Parse(Id), newName);


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

        private void OnClickProfile(object sender, ItemClickEventArgs e)
        {
            PlayerProfile profile = e.ClickedItem as PlayerProfile;

            PlayerIdTextBlock.Text = profile.Id.ToString();
            PlayerNameTextBlock.Text = profile.PlayerName;

            SelectProfileButton.IsEnabled = true;
            EditProfileButton.IsEnabled = true;
            DeleteProfileButton.IsEnabled = true;
        }

        private async void OnSelectProfileAsync(object sender, RoutedEventArgs e)
        {
            string Id = PlayerIdTextBlock.Text;

            if (Id.Equals(string.Empty))
            {
                return;
            }

            await ViewModel.SetSelectedPlayerAsync(Int32.Parse(Id));
            PlayerProfileViewModel.SelectedPlayerChangedEvent.Invoke(this, EventArgs.Empty);
        }
    }
}

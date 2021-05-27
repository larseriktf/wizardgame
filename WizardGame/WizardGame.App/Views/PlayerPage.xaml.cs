using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using WizardGame.App.Helpers;
using WizardGame.App.ViewModels;
using WizardGame.Model;

namespace WizardGame.App.Views
{
    public sealed partial class PlayerPage : Page
    {
        public delegate void SelectedPlayerChangedEventHandler(object sender, EventArgs e);
        public PlayerViewModel ViewModel { get; } = new PlayerViewModel();

        public PlayerPage()
        {
            DataContext = this;
            InitializeComponent();
        }

        private async void OnLoadedAsync(object sender, RoutedEventArgs e)
        {
            await ViewModel.LoadAllPlayersAsync();
            ControlHandler.ToggleVisibility(PlayerProgressRIng);
        }

        private async void OnAddPlayerAsync(object sender, RoutedEventArgs e)
        {
            // Get objects
            Button btn = sender as Button;
            TextBox textBox = btn.Tag as TextBox;

            // Adds
            await ViewModel.AddNewPlayerAsync(textBox.Text);

            // Clear textBox
            textBox.Text = string.Empty;
        }


        private async void OnDeletePlayerAsync(object sender, RoutedEventArgs e)
        {
            string Id = PlayerIdTextBlock.Text;

            if (Id.Length == 0)
            {
                return;
            }

            await ViewModel.DeletePlayerAsync(Int32.Parse(Id));

            ClearPlayerTextBoxes();
        }

        private void ClearPlayerTextBoxes() =>
            PlayerIdTextBlock.Text = PlayerNameTextBlock.Text = string.Empty;

        private async void OnApplyEditPlayerAsync(object sender, RoutedEventArgs e)
        {
            string Id = PlayerIdTextBlock.Text;
            string newName = UpdatedPlayerNameTextBox.Text;

            if (Id.Length == 0)
            {
                return;
            }

            await ViewModel.UpdatePlayerAsync(Int32.Parse(Id), newName);


            UpdatedPlayerNameTextBox.Text = string.Empty;

            OnTogglePlayerAsync(sender, e);
        }

        private void OnTogglePlayerAsync(object sender, RoutedEventArgs e)
        {
            ControlHandler.ToggleVisibility(PlayerInfoPanel);
            ControlHandler.ToggleVisibility(EditPlayerPanel);
        }

        private void OnClickPlayer(object sender, ItemClickEventArgs e)
        {
            Player player = e.ClickedItem as Player;

            PlayerIdTextBlock.Text = player.Id.ToString();
            PlayerNameTextBlock.Text = player.PlayerName;

            SelectPlayerButton.IsEnabled = true;
            EditPlayerButton.IsEnabled = true;
            DeletePlayerButton.IsEnabled = true;
        }

        private async void OnSelectPlayerAsync(object sender, RoutedEventArgs e)
        {
            string Id = PlayerIdTextBlock.Text;

            if (Id.Equals(string.Empty))
            {
                return;
            }

            await ViewModel.SetSelectedPlayerAsync(Int32.Parse(Id));
            PlayerViewModel.SelectedPlayerChangedEvent.Invoke(this, EventArgs.Empty);
        }
    }
}

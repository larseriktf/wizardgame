using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using WizardGame.App.DataAccess;
using WizardGame.App.Services;
using WizardGame.App.ViewModels;
using WizardGame.Model;

namespace WizardGame.App.Views
{
    public sealed partial class SettingsPage : Page
    {
        public GamePageViewModel ViewModel { get; } = new GamePageViewModel();

        public SettingsPage()
        {
            InitializeComponent();

            Loaded += OnLoadedAsync;
        }

        private async void OnLoadedAsync(object sender, RoutedEventArgs e)
        {
            await ViewModel.LoadConfigurationsAsync();
        }

        private void OnInspectConfiguration(object sender, ItemClickEventArgs e)
        {

        }
    }
}

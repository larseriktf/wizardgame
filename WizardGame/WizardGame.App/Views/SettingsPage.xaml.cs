using System;
using System.Diagnostics;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
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
            await ViewModel.LoadAllConfigurationsAsync();
        }

        private async void OnInspectConfigurationAsync(object sender, RoutedEventArgs e)
        {
            var Id = (sender as Button).Tag as int?;

            Debug.WriteLine(Id);

            await ViewModel.LoadSpecificConfigurationAsync(Id);
        }
    }
}

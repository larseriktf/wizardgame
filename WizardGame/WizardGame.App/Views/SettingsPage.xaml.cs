using System;

using Windows.UI.Xaml.Controls;
using WizardGame.App.Services;
using WizardGame.App.ViewModels;

namespace WizardGame.App.Views
{
    public sealed partial class SettingsPage : Page
    {

        public SettingsPage()
        {
            InitializeComponent();
        }

        private void OnClickToGoBack(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            if (NavigationService.CanGoBack)
            {
                NavigationService.GoBack();
            }
        }

        private void OnInspectConfiguration(object sender, ItemClickEventArgs e)
        {

        }
    }
}

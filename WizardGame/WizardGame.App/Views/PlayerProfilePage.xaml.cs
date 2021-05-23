using System;

using Windows.UI.Xaml.Controls;

using WizardGame.App.ViewModels;

namespace WizardGame.App.Views
{
    public sealed partial class PlayerProfilePage : Page
    {
        public PlayerProfilePageViewModel ViewModel { get; } = new PlayerProfilePageViewModel();

        public PlayerProfilePage()
        {
            InitializeComponent();
        }
    }
}

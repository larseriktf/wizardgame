using System;

using Windows.UI.Xaml.Controls;

using WizardGame.App.ViewModels;

namespace WizardGame.App.Views
{
    public sealed partial class LeaderboardsPage : Page
    {
        public LeaderboardsViewModel ViewModel { get; } = new LeaderboardsViewModel();

        public LeaderboardsPage()
        {
            InitializeComponent();
        }
    }
}

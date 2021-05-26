using System;

using Windows.UI.Xaml.Controls;

using WizardGame.App.ViewModels;

namespace WizardGame.App.Views
{
    public sealed partial class LeaderboardsPage : Page
    {
        public GameStatisticViewModel ViewModel { get; } = new GameStatisticViewModel();

        public LeaderboardsPage()
        {
            InitializeComponent();
        }
    }
}

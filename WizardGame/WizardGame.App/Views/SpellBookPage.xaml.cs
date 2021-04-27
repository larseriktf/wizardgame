using System;

using Windows.UI.Xaml.Controls;

using WizardGame.App.ViewModels;

namespace WizardGame.App.Views
{
    public sealed partial class SpellBookPage : Page
    {
        public BlankViewModel ViewModel { get; } = new BlankViewModel();

        public SpellBookPage()
        {
            InitializeComponent();
        }
    }
}

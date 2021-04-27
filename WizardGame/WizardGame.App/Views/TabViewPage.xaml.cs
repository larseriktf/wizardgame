using System;

using Windows.UI.Xaml.Controls;

using WizardGame.App.ViewModels;

namespace WizardGame.App.Views
{
    // For more info about the TabView Control see
    // https://docs.microsoft.com/uwp/api/microsoft.ui.xaml.controls.tabview?view=winui-2.2
    // For other samples, get the XAML Controls Gallery app http://aka.ms/XamlControlsGallery
    public sealed partial class TabViewPage : Page
    {
        public TabViewViewModel ViewModel { get; } = new TabViewViewModel();

        public TabViewPage()
        {
            InitializeComponent();
        }
    }
}

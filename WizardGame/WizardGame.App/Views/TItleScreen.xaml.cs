using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using WizardGame.App.Services;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace WizardGame.App.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class TitleScreen : Page
    {
        public TitleScreen() => InitializeComponent();

        private void OnStartGame(object sender, RoutedEventArgs e) => NavigationService.Navigate<GamePage>();
    }
}

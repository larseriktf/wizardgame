using Microsoft.Graphics.Canvas.UI;
using Microsoft.Graphics.Canvas.UI.Xaml;
using System;
using System.Linq;
using System.Threading.Tasks;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using WizardGame.App.GameFiles;
using WizardGame.App.GameFiles.Entities.Dev;
using WizardGame.App.GameFiles.Entities.Player;
using WizardGame.App.GameFiles.Graphics;
using WizardGame.App.GameFiles.Input;
using WizardGame.App.Interfaces;
using WizardGame.App.ViewModels;

namespace WizardGame.App.Views
{
    public sealed partial class GamePage : Page
    {
        public GameDataViewModel GameViewModel = new GameDataViewModel();
        public PlayerViewModel PlayerViewModel = new PlayerViewModel();

        public GamePage()
        {
            DataContext = this;
            PlayerViewModel.SelectedPlayerChangedEvent += OnSelectedPlayerChangedEventAsync;
            InitializeComponent();
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            LoadPageBasicsAsync();

            // Subscribe keyboard input
            Window.Current.CoreWindow.KeyDown += OnKeyDownUIThread;
            Window.Current.CoreWindow.KeyUp += OnKeyUpUIThread;

            // Start game timer
            GameManager.GameTimer.Start();
        }
        private async void LoadPageBasicsAsync()
        {
            await PlayerViewModel.LoadSelectedPlayerAsync();

            SelectedPlayerProgressRing.Visibility = Visibility.Collapsed;
            SelectedPlayerContentControl.Visibility = Visibility.Visible;

            StartGameButton.IsEnabled = true;
            LeaderboardsButton.IsEnabled = true;
        }

        void OnUnloaded(object sender, RoutedEventArgs e)
        {
            // Best practice: Prevent simple memory leak
            canvas.RemoveFromVisualTree();
            canvas = null;

            // Unsubscribe keyboard input
            Window.Current.CoreWindow.KeyDown -= OnKeyDownUIThread;
            Window.Current.CoreWindow.KeyDown -= OnKeyUpUIThread;

            // Stop game timer
            GameManager.GameTimer.Stop();
            GameManager.ElapsedTime = TimeSpan.FromMilliseconds(GameManager.GameTimer.ElapsedMilliseconds);

            // Save game
            SaveGameAsync();
        }


        private void OnCreateResources(CanvasAnimatedControl sender, CanvasCreateResourcesEventArgs args) =>
            args.TrackAsyncAction(LoadResourcesAsync(sender).AsAsyncAction());

        async Task LoadResourcesAsync(CanvasAnimatedControl sender)
        {   // Loads images and spritesheets
            // Pre-load image resources
            await ImageLoader.LoadImageResourceAsync(sender.Device);

            Ghost.Spawner(10 * 128 + 64, 6 * 128 + 64);

            // Add enemy spawners
            EnemySpawner.Spawner(2 * 128 + 64, 5 * 128 + 64);
            EnemySpawner.Spawner(12 * 128 + 64, 5 * 128 + 64);

            // Generate and load maps
            MapEditor.MakeMaps();
            MapEditor.LoadMap(0, sender.Device);
        }


        private void OnUpdate(ICanvasAnimatedControl sender, CanvasAnimatedUpdateEventArgs args)
        {
            if (GameManager.EnemyCounter <= 0)
            {
                GameManager.NextWave();
            }

            foreach (IDrawable entity in EntityManager.Entities.ToList())
            {
                entity.Update();
            }
        }

        private void OnDraw(ICanvasAnimatedControl sender, CanvasAnimatedDrawEventArgs args)
        {
            var ds = args.DrawingSession;

            foreach (IDrawable entity in EntityManager.Entities.ToList())
            {
                entity.Draw(ds);
            }

            CanvasDebugger.DrawMessages(ds);
            CanvasDebugger.TestDrawing(ds);
        }

        private void OnKeyDownUIThread(CoreWindow sender, KeyEventArgs args) => ConfigureKeyboardInput(args, true);

        private void OnKeyUpUIThread(CoreWindow sender, KeyEventArgs args) => ConfigureKeyboardInput(args, false);

        private void ConfigureKeyboardInput(KeyEventArgs args, bool state)
        {
            args.Handled = true;
            var action = canvas.RunOnGameLoopThreadAsync(() => KeyBoard.ConfigureInputKey(args.VirtualKey, state));
        }

        private void OnToggleMenu(object sender, RoutedEventArgs e)
        {
            ToggleVisibility(MainMenu);

            if (MainMenu.Visibility == Visibility.Collapsed)
            {
                canvas.Paused = false;
                GameManager.GameTimer.Start();
            }
            else
            {
                canvas.Paused = true;
                GameManager.GameTimer.Stop();
            }
        }

        private void OnToggleExitWindow(object sender, RoutedEventArgs e) => ToggleVisibility(ComfirmExitGrid);

        private void ToggleVisibility(UIElement control)
        {
            if (control.Visibility == Visibility.Visible)
            {
                control.Visibility = Visibility.Collapsed;
            }
            else
            {
                control.Visibility = Visibility.Visible;
            }
        }

        private async void SaveGameAsync() =>
            await GameViewModel.AddPlayerGameAsync(
                PlayerViewModel.SelectedPlayer.Id,
                GameManager.Wave,
                GameManager.EnemiesDefeated,
                GameManager.ElapsedTime);

        public async void OnSelectedPlayerChangedEventAsync(object sender, EventArgs e)
        {
            await PlayerViewModel.LoadSelectedPlayerAsync();
            PlayerViewModel.SelectedPlayer = (sender as PlayerProfilePage).ViewModel.SelectedPlayer;
        }

        private void OnSizeChanged(object sender, SizeChangedEventArgs e) => Screen.Width = e.NewSize.Width;

        // Menu navigation methods
        private void OnOpenSpellBook(object sender, RoutedEventArgs e) =>
            MenuFrame.Navigate(typeof(SpellBookPage));

        private void OnOpenPlayerProfile(object sender, RoutedEventArgs e) =>
            MenuFrame.Navigate(typeof(PlayerProfilePage));

        private void OnOpenLeaderboards(object sender, RoutedEventArgs e) =>
            MenuFrame.Navigate(typeof(LeaderboardsPage), PlayerViewModel.SelectedPlayer);

        private void OnOpenSettings(object sender, RoutedEventArgs e) =>
            MenuFrame.Navigate(typeof(SettingsPage));

        private void OnComfirmExit(object sender, RoutedEventArgs e) => Application.Current.Exit();
    }
}

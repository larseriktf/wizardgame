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
using WizardGame.App.Helpers;
using WizardGame.App.Interfaces;
using WizardGame.App.ViewModels;
using Windows.System;
using WizardGame.App.GameFiles.Entities.Enemies;
using static WizardGame.App.GameFiles.EntityManager;
using WizardGame.App.GameFiles.Entities.HudElements;
using WizardGame.App.GameFiles.Entities;

namespace WizardGame.App.Views
{
    public sealed partial class GamePage : Page
    {
        public GameDataViewModel GameViewModel = new GameDataViewModel();
        public PlayerViewModel PlayerViewModel = new PlayerViewModel();
        private bool RunGamePlayLoop = false;

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

            // Save game
            SaveGameAsync();
        }


        private void OnCreateResources(CanvasAnimatedControl sender, CanvasCreateResourcesEventArgs args) =>
            args.TrackAsyncAction(LoadResourcesAsync(sender).AsAsyncAction());

        async Task LoadResourcesAsync(CanvasAnimatedControl sender)
        {   // Loads images and spritesheets
            // Pre-load image resources
            await ImageLoader.LoadImageResourceAsync(sender.Device);

            InitialSetup();
        }

        private void InitialSetup()
        {
            // Add starting entities
            AddEntity("layer0", new Target(7 * 128 + 64, 3 * 128 + 64));
            AddEntity("layer1", new Ghost(8 * 128 + 64, 5 * 128 + 64));
            AddEntity("layer_hud", new HealthBar());
            AddEntity("layer_hud", new CrystalOrb());

            // Add enemy spawners
            AddEntity("layer1", new EnemySpawner(2 * 128 + 64, 5 * 128 + 64));
            AddEntity("layer1", new EnemySpawner(12 * 128 + 64, 5 * 128 + 64));

            // Generate and load maps
            MapEditor.MakeMaps();
            MapEditor.LoadMap(0);
        }


        private void OnUpdate(ICanvasAnimatedControl sender, CanvasAnimatedUpdateEventArgs args)
        {
            if (Ghost.HP <= 0)
            {   // End current game
                GameOver();
            }

            GameManager.TotalEnemies = GameManager.NormalEnemies + GameManager.SpecialEnemies;

            if (GameManager.TotalEnemies <= 0 && RunGamePlayLoop == true)
            {
                GameManager.NextWave();
            }

            foreach (IDrawable entity in Entities.ToList())
            {
                entity.Update();
            }
        }

        private void OnDraw(ICanvasAnimatedControl sender, CanvasAnimatedDrawEventArgs args)
        {
            var ds = args.DrawingSession;

            foreach (IDrawable entity in Entities.ToList())
            {
                entity.Draw(ds);
            }

            CanvasDebugger.DrawMessages(ds);
            CanvasDebugger.TestDrawing(ds);
        }

        private void GameOver()
        {
            SaveGameAsync();
            GameManager.GameTimer.Restart();
            RestartGamePlayLoop();
        }

        private void RestartGamePlayLoop()
        {
            foreach (Entity entity in Entities.ToList())
            {
                if (entity is Enemy)
                {
                    RemoveEntity(entity);
                }
            }

            GameManager.Wave = 0;
            GameManager.NormalEnemies = 0;
            GameManager.EnemiesDefeated = 0;
            GameManager.ElapsedTime = new TimeSpan(0, 0, 0);

            canvas.Paused = true;
        }

        private void OnKeyDownUIThread(CoreWindow sender, KeyEventArgs args) => ConfigureKeyboardInput(args, true);

        private void OnKeyUpUIThread(CoreWindow sender, KeyEventArgs args) => ConfigureKeyboardInput(args, false);

        private void ConfigureKeyboardInput(KeyEventArgs args, bool state)
        {
            // Toggle pause
            if (args.VirtualKey == VirtualKey.Escape && state == true && RunGamePlayLoop == true)
            {
                ToggleMenu();
            }

            // Detect key presses
            args.Handled = true;
            var action = canvas.RunOnGameLoopThreadAsync(() => KeyBoard.ConfigureInputKey(args.VirtualKey, state));
        }

        private void OnToggleGame(object sender, RoutedEventArgs e)
        {
            if (RunGamePlayLoop == false)
            {
                StartGameButton.Content = "RESUME";
                RunGamePlayLoop = true;
            }

            ToggleMenu();
        }

        private void ToggleMenu()
        {
            ControlHandler.ToggleVisibility(MainMenu);
            MenuFrame.Content = null;

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

        private void OnToggleExitWindow(object sender, RoutedEventArgs e) =>
            ControlHandler.ToggleVisibility(ComfirmExitGrid);


        private async void SaveGameAsync()
        {
            // Stop game timer
            GameManager.GameTimer.Stop();
            GameManager.ElapsedTime = TimeSpan.FromMilliseconds(GameManager.GameTimer.ElapsedMilliseconds);

            // Save game data
            await GameViewModel.AddPlayerGameAsync(
                PlayerViewModel.SelectedPlayer.Id,
                GameManager.Wave,
                GameManager.EnemiesDefeated,
                GameManager.ElapsedTime);
        }
            

        public async void OnSelectedPlayerChangedEventAsync(object sender, EventArgs e)
        {
            await PlayerViewModel.LoadSelectedPlayerAsync();
            PlayerViewModel.SelectedPlayer = (sender as PlayerPage).ViewModel.SelectedPlayer;
        }

        private void OnSizeChanged(object sender, SizeChangedEventArgs e) => Screen.Width = e.NewSize.Width;

        // Menu navigation methods
        private void OnOpenSpellBook(object sender, RoutedEventArgs e) =>
            MenuFrame.Navigate(typeof(SpellBookPage));

        private void OnOpenPlayerProfile(object sender, RoutedEventArgs e) =>
            MenuFrame.Navigate(typeof(PlayerPage));

        private void OnOpenLeaderboards(object sender, RoutedEventArgs e) =>
            MenuFrame.Navigate(typeof(LeaderboardsPage), PlayerViewModel.SelectedPlayer);

        private void OnOpenSettings(object sender, RoutedEventArgs e) =>
            MenuFrame.Navigate(typeof(SettingsPage));

        private void OnComfirmExit(object sender, RoutedEventArgs e) => Application.Current.Exit();
    }
}

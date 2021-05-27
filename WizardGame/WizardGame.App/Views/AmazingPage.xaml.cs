using Microsoft.Graphics.Canvas.UI.Xaml;
using System;
using System.Linq;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using WizardGame.App.Classes;
using WizardGame.App.Classes.Graphics;
using WizardGame.App.Classes.Input;
using WizardGame.App.Interfaces;
using Windows.UI.Core;
using System.Diagnostics;
using WizardGame.App.Services;
using WizardGame.App.ViewModels;
using WizardGame.App.Classes.Entities.Enemies;
using WizardGame.App.Classes.Entities;
using Windows.Graphics.Display;
using Windows.Foundation;
using WizardGame.App.Classes.Entities.Dev;
using WizardGame.Model;
using Windows.UI.Xaml.Navigation;
using Microsoft.Graphics.Canvas.UI;

namespace WizardGame.App.Views
{
    public sealed partial class AmazingPage : Page
    {
        public GameStatisticViewModel ViewModel = new GameStatisticViewModel();
        public PlayerProfile SelectedPlayer { get; set; }

        public AmazingPage()
        {
            DataContext = this;
            InitializeComponent();

            
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            try
            {
                SelectedPlayer = e.Parameter as PlayerProfile;
            }
            catch (Exception exception)
            {
                SelectedPlayer = new PlayerProfile()
                {
                    Id = 0,
                    PlayerName = "Undefined",
                    IsSelected = true,
                    GameStatistics = null
                };
                Console.WriteLine(exception.StackTrace);
            }

            SelectedPlayerProgressRing.Visibility = Visibility.Collapsed;
            SelectedPlayerStackPanel.Visibility = Visibility.Visible;
            SelectedPlayerNameTextBlock.Text = SelectedPlayer.PlayerName;
            base.OnNavigatedTo(e);
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            // Subscribe keyboard input
            Window.Current.CoreWindow.KeyDown += OnKeyDownUIThread;
            Window.Current.CoreWindow.KeyUp += OnKeyUpUIThread;

            // Start game timer
            GameManager.GameTimer.Start();
        }

        void OnUnloaded(object sender, RoutedEventArgs e)
        {   // Best practice: Prevent simple memory leak
            

            // Unsubscribe keyboard input
            Window.Current.CoreWindow.KeyDown -= OnKeyDownUIThread;
            Window.Current.CoreWindow.KeyDown -= OnKeyUpUIThread;

            // Stop game timer
            GameManager.GameTimer.Stop();
            GameManager.ElapsedTime = TimeSpan.FromMilliseconds(GameManager.GameTimer.ElapsedMilliseconds);

            // Save game
            SaveGameAsync();

            canvas.RemoveFromVisualTree();
            canvas = null;
        }

        private void OnKeyDownUIThread(CoreWindow sender, KeyEventArgs args)
        {
            args.Handled = true;

            var action = canvas.RunOnGameLoopThreadAsync(() => KeyBoard.ConfigureInputKey(args.VirtualKey, true));
        }

        private void OnKeyUpUIThread(CoreWindow sender, KeyEventArgs args)
        {
            args.Handled = true;

            var action = canvas.RunOnGameLoopThreadAsync(() => KeyBoard.ConfigureInputKey(args.VirtualKey, false));
        }

        private void OnCreateResources(CanvasAnimatedControl sender, CanvasCreateResourcesEventArgs args) =>
            args.TrackAsyncAction(LoadResourcesAsync(sender).AsAsyncAction());

        async Task LoadResourcesAsync(CanvasAnimatedControl sender)
        {   // Loads images and spritesheets
            // Pre-load image resources
            await ImageLoader.LoadImageResourceAsync(sender.Device);

            Player.Spawner(400, 400);
            //EntityManager.AddEntity("layer1", new Cactus()
            //{
            //    X = (8 * 128) + 64,
            //    Y = (4 * 128) + 96
            //});

            // Add enemy spawners
            EnemySpawner.Spawner((2 * 128) + 64, (5 * 128) + 64);
            EnemySpawner.Spawner((12 * 128) + 64, (5 * 128) + 64);

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

        private void OnTogglePause(object sender, RoutedEventArgs e)
        {
            if (PausedMenu.Visibility == Visibility.Visible)
            {
                PausedMenu.Visibility = Visibility.Collapsed;
                GameFrame.Content = null;
                canvas.Paused = false;
                GameManager.GameTimer.Start();
            }
            else
            {
                PausedMenu.Visibility = Visibility.Visible;
                canvas.Paused = true;
                GameManager.GameTimer.Stop();
            }
        }

        private void OnOpenSpellBook(object sender, RoutedEventArgs e) =>
            GameFrame.Navigate(typeof(SpellBookPage));

        private void OnOpenSettings(object sender, RoutedEventArgs e) =>
            GameFrame.Navigate(typeof(SettingsPage));

        private void OnSizeChanged(object sender, SizeChangedEventArgs e) =>
            Screen.Width = e.NewSize.Width;

        private void OnToggleExitWindow(object sender, RoutedEventArgs e)
        {
            if (ComfirmExitGrid.Visibility == Visibility.Visible)
            {
                ComfirmExitGrid.Visibility = Visibility.Collapsed;
            }
            else
            {
                ComfirmExitGrid.Visibility = Visibility.Visible;
            }
        }

        private void OnComfirmExit(object sender, RoutedEventArgs e) =>
            NavigationService.Navigate<TitleScreen>();

        private async void SaveGameAsync()
        {
            await ViewModel.AddPlayerGameAsync(
                SelectedPlayer.Id,
                GameManager.Wave,
                GameManager.EnemiesDefeated,
                GameManager.ElapsedTime);
        }
    }
}

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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace WizardGame.App.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class GamePage : Page
    {
        public GamePageViewModel ViewModel { get; } = new GamePageViewModel();
        public CanvasAnimatedControl Canvas
        {
            get
            {
                return canvas;
            }
        }

        public GamePage()
        {
            InitializeComponent();
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            // Subscribe keyboard input
            Window.Current.CoreWindow.KeyDown += KeyDown_UIThread;
            Window.Current.CoreWindow.KeyUp += KeyUp_UIThread;
        }

        void OnUnloaded(object sender, RoutedEventArgs e)
        {   // Best practice: Prevent simple memory leak
            canvas.RemoveFromVisualTree();
            canvas = null;

            // Unsubscribe keyboard input
            Window.Current.CoreWindow.KeyDown -= KeyDown_UIThread;
            Window.Current.CoreWindow.KeyDown -= KeyUp_UIThread;
        }

        private void KeyDown_UIThread(CoreWindow sender, KeyEventArgs args)
        {
            args.Handled = true;

            // @TODO: Move this out of this event handler
            if (args.VirtualKey == Windows.System.VirtualKey.Escape)
            {
                PausedMenu.Visibility = Visibility.Collapsed;
            }

            var action = canvas.RunOnGameLoopThreadAsync(() => KeyBoard.ConfigureInputKey(args.VirtualKey, true));
        }

        private void KeyUp_UIThread(CoreWindow sender, KeyEventArgs args)
        {
            args.Handled = true;

            var action = canvas.RunOnGameLoopThreadAsync(() => KeyBoard.ConfigureInputKey(args.VirtualKey, false));
        }

        private void OnCreateResources(CanvasAnimatedControl sender, Microsoft.Graphics.Canvas.UI.CanvasCreateResourcesEventArgs args)
        {   // Creates Resources once
            // Load Images
            args.TrackAsyncAction(LoadResourcesAsync(sender).AsAsyncAction());
        }

        async Task LoadResourcesAsync(CanvasAnimatedControl sender)
        {   // Loads images and spritesheets
            // Pre-load image resources
            await ImageLoader.LoadImageResourceAsync(sender.Device);

            EntityManager.AddEntity("layer1", new Player()
            {
                X = 400,
                Y = 400
            });
            //EntityManager.AddEntity("layer1", new Bunny()
            //{
            //    X = 1000,
            //    Y = 200
            //});
            EntityManager.AddEntity("layer1", new Cactus()
            {
                X = (8 * 128) + 64,
                Y = (4 * 128) + 96
            });

            // Generate and load maps
            MapEditor.MakeMaps();
            MapEditor.LoadMap(0, sender.Device);
        }

        private void OnUpdate(ICanvasAnimatedControl sender, CanvasAnimatedUpdateEventArgs args)
        {
            if (GameStateManager.EnemyCounter <= 0)
            {
                GameStateManager.NextWave();
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

        private void OnTogglePauseMenu(object sender, RoutedEventArgs e)
        {
            if (PausedMenu.Visibility == Visibility.Visible)
            {
                PausedMenu.Visibility = Visibility.Collapsed;
                DimBox.Visibility = Visibility.Collapsed;
                GameFrame.Content = null;
                canvas.Paused = false;
            }
            else
            {
                PausedMenu.Visibility = Visibility.Visible;
                DimBox.Visibility = Visibility.Visible;
                canvas.Paused = true;
            }
        }

        private void OnOpenSpellBook(object sender, RoutedEventArgs e)
        {
            GameFrame.Navigate(typeof(SpellBookPage));
        }

        private void OnOpenSettings(object sender, RoutedEventArgs e)
        {
            GameFrame.Navigate(typeof(SettingsPage));
        }

        private void OnOpenMainMenu(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate<TitleScreen>();
        }

        private void OnSaveAndQuit(object sender, RoutedEventArgs e)
        {
            Windows.UI.Xaml.Application.Current.Exit();
        }

        private void OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            Screen.Width = e.NewSize.Width;
        }
    }
}

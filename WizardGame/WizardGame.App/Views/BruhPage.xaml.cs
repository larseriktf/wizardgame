using Microsoft.Graphics.Canvas.UI.Xaml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using WizardGame.App.Classes;
using WizardGame.App.Classes.Entities;
using WizardGame.App.Classes.Entities.Dev;
using WizardGame.App.Classes.Graphics;
using WizardGame.App.Services;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace WizardGame.App.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class BruhPage : Page
    {
        Task levelLoadTask;

        public BruhPage()
        {
            this.InitializeComponent();
        }

        private void canvas_CreateResources(Microsoft.Graphics.Canvas.UI.Xaml.CanvasAnimatedControl sender, Microsoft.Graphics.Canvas.UI.CanvasCreateResourcesEventArgs args)
        {
            args.TrackAsyncAction(LoadResourcesAsync(sender).AsAsyncAction());
        }

        async Task LoadResourcesAsync(CanvasAnimatedControl sender)
        {   // Loads images and spritesheets

            if (levelLoadTask != null)
            {
                levelLoadTask.AsAsyncAction().Cancel();
                try
                {
                    await levelLoadTask;
                }
                catch {}
                levelLoadTask = null;
            }

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

        private void canvas_Update(Microsoft.Graphics.Canvas.UI.Xaml.ICanvasAnimatedControl sender, Microsoft.Graphics.Canvas.UI.Xaml.CanvasAnimatedUpdateEventArgs args)
        {

        }

        private void canvas_Draw(Microsoft.Graphics.Canvas.UI.Xaml.ICanvasAnimatedControl sender, Microsoft.Graphics.Canvas.UI.Xaml.CanvasAnimatedDrawEventArgs args)
        {

        }

        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            canvas.RemoveFromVisualTree();
            canvas = null;
        }

        private void Button_Click(object sender, RoutedEventArgs e) =>
            NavigationService.Navigate<TitleScreen>();

        private void OnSizeChanged(object sender, SizeChangedEventArgs e) =>
            Screen.Width = e.NewSize.Width;
    }
}

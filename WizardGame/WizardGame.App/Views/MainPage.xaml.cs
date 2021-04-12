using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI;
using System.Numerics;
using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.Effects;
using Windows.System;
using Windows.UI.Core;
using WizardGame.App.Classes;
using Microsoft.Graphics.Canvas.UI.Xaml;
using WizardGame.App.Classes.Entities;
using WizardGame.App.Interfaces;


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace WizardGame.App.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        readonly DispatcherTimer gameTimer = new DispatcherTimer();

        public MainPage()
        {
            gameTimer.Tick += GameTimerEvent;
            gameTimer.Interval = TimeSpan.FromMilliseconds(16);
            gameTimer.Start();

            InitializeComponent();
        }

        private void GameTimerEvent(object sender, object e)
        {   // Every tick
            TrackKeyboard();
        }

        private void TrackKeyboard()
        {
            KeyBoard.UpdateKeys();
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {

        }

        void OnUnloaded(object sender, RoutedEventArgs e)
        {   // Best practice: Prevent simple memory leak
            canvas.RemoveFromVisualTree();
            canvas = null;
        }

        private void OnCreateResources(CanvasAnimatedControl sender, Microsoft.Graphics.Canvas.UI.CanvasCreateResourcesEventArgs args)
        {   // Creates Resources once
            // Load Images
            args.TrackAsyncAction(LoadResourcesAsync(sender).AsAsyncAction());
        }

        async Task LoadResourcesAsync(CanvasAnimatedControl sender)
        {   // Loads images and spritesheets

            EntityManager.gameEntities.Add(new Player()
            {
                X = 400,
                Y = 400
            });

            //CardEnemy.Spawner(1200, 500, 64);

            // Add Maps
            MapEditor.MakeMaps();
            MapEditor.LoadMap(0, sender.Device);

            // Add stuff
            Layer layer1 = new Layer("layer1");

            foreach (Entity entity in EntityManager.gameEntities)
            {
                layer1.GameObjects.Add(entity);
            }


            EntityManager.Layers.Add(layer1);

            // Load sprites and spritesheets
            foreach (Layer layer in EntityManager.Layers)
            {
                foreach (IDrawable gameObject in layer.GameObjects)
                {
                    gameObject.LoadImageResourceAsync(sender.Device);
                }
            }
        }



        private void OnDraw(ICanvasAnimatedControl sender, CanvasAnimatedDrawEventArgs args)
        {
            var ds = args.DrawingSession;

            // Draw gameObjects
            foreach (Layer layer in EntityManager.Layers)
            {
                foreach (IDrawable gameObject in layer.GameObjects)
                {
                    gameObject.DrawSelf(ds);
                }
            }

            CanvasDebugger.DrawMessages(ds);
            CanvasDebugger.TestDrawing(ds);
        }
    }
}

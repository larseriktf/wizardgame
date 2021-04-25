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
using WizardGame.App.Classes.Entities.Characters;
using System.Text;
using WizardGame.App.Classes.Entities.Spells;
using WizardGame.App.Classes.Graphics;
using WizardGame.App.Classes.Input;


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
            InitializeComponent();
        }

        private void Step(object sender, object e)
        {   // Every tick
            KeyBoard.UpdateKeys();


        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            gameTimer.Tick += Step;
            gameTimer.Interval = TimeSpan.FromMilliseconds(16);
            gameTimer.Start();
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

            // Generate and load maps
            MapEditor.MakeMaps();
            MapEditor.LoadMap(0, sender.Device);
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
    }
}

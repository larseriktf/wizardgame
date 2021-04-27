using Microsoft.Graphics.Canvas.UI.Xaml;
using System;
using System.Linq;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using WizardGame.App.Classes;
using WizardGame.App.Classes.Entities.Characters;
using WizardGame.App.Classes.Graphics;
using WizardGame.App.Classes.Input;
using WizardGame.App.Interfaces;


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace WizardGame.App.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Game : Page
    {
        readonly DispatcherTimer gameTimer = new DispatcherTimer();

        public Game()
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
            EntityManager.AddEntity("layer1", new Bunny()
            {
                X = 1000,
                Y = 200
            });

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

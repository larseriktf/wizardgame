using Microsoft.Graphics.Canvas;
using Windows.UI;
using WizardGame.App.Interfaces;

namespace WizardGame.App.Classes.Entities.Dev
{
    public class Solid : Entity, IDrawable
    {
        public Solid()
        {
            Width = 128;
            Height = 128;
        }

        public void Update()
        {

        }

        public void Draw(CanvasDrawingSession ds)
        {
            ds.DrawRectangle(X - Width / 2, Y - Height / 2, Width, Height, Colors.Green);
            //ds.DrawRectangle(X, Y, Width, Height, Colors.Blue);
        }
    }
}

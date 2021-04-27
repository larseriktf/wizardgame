using Microsoft.Graphics.Canvas;
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

        public void Draw(CanvasDrawingSession ds)
        {
            //ds.DrawRectangle(X, Y, Width, Height, Colors.Blue);
        }
    }
}

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
            OffsetAndScale();
        }

        public void Draw(CanvasDrawingSession ds)
        {
            ds.DrawRectangle(OffsetX - OffsetWidth / 2, OffsetY - OffsetHeight / 2, OffsetWidth, OffsetHeight, Colors.Green);
            //ds.DrawRectangle(X, Y, Width, Height, Colors.Blue);
        }
    }
}

using Microsoft.Graphics.Canvas;
using Windows.UI;
using WizardGame.App.Interfaces;

namespace WizardGame.App.Classes.Entities.Dev
{
    public class Solid : Entity, IDrawable
    {
        public Solid(float x, float y) : base(x, y, 128, 128) {}

        public void Update()
        {
            OffsetAndScale();
        }

        public void Draw(CanvasDrawingSession ds)
        {
            ds.DrawRectangle(OffsetX - OffsetWidth / 2, OffsetY - OffsetHeight / 2, OffsetWidth, OffsetHeight, Colors.Green);
        }
    }
}

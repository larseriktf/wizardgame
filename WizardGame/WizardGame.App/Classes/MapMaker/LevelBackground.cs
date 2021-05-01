using Microsoft.Graphics.Canvas;
using WizardGame.App.Classes.Entities;
using WizardGame.App.Interfaces;

namespace WizardGame.App.Classes.MapMaker
{
    public class LevelBackground : Entity, IDrawable
    {
        public string BitMapUri { get; }
        private readonly CanvasBitmap bitmap;

        public LevelBackground(CanvasBitmap bitmap)
        {
            this.bitmap = bitmap;
        }

        public void Update()
        {

        }

        public void Draw(CanvasDrawingSession ds)
        {
            if (bitmap != null)
            {
                ds.DrawImage(bitmap, X, Y);
            }
        }
    }
}

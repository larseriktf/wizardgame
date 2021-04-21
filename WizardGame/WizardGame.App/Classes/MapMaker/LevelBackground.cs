using Microsoft.Graphics.Canvas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public void Draw(CanvasDrawingSession ds)
        {
            if (bitmap != null)
            {
                ds.DrawImage(bitmap, X, Y);
            }
        }
    }
}

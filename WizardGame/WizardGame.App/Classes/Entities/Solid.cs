using Microsoft.Graphics.Canvas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using WizardGame.App.Interfaces;

namespace WizardGame.App.Classes.Entities
{
    public class Solid : Entity, IDrawable
    {
        public Solid()
        {
            Width = 128;
            Height = 128;
        }

        public void DrawSelf(CanvasDrawingSession ds)
        {
            ds.DrawRectangle(X, Y, Width, Height, Colors.Blue);
        }

        public void LoadImageResourceAsync(CanvasDevice device)
        {
            
        }
    }
}

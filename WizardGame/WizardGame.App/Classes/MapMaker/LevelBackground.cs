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
        public CanvasBitmap BitMap;
        public string BitMapUri { get; }

        public LevelBackground(string bitMapUri)
        {
            BitMapUri = bitMapUri;
        }

        public async void LoadImageResourceAsync(CanvasDevice device)
        {
            BitMap = await CanvasBitmap.LoadAsync(device, new Uri(BitMapUri));
        }

        public void Draw(CanvasDrawingSession ds)
        {
            if (BitMap != null)
            {
                ds.DrawImage(BitMap, X, Y);
            }
        }
    }
}

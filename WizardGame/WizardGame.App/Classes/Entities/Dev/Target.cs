using Microsoft.Graphics.Canvas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using WizardGame.App.Interfaces;
using static System.Math;

namespace WizardGame.App.Classes.Entities.Dev
{
    public class Target : Entity, IDrawable
    {
        public CanvasBitmap BitMap;
        public string BitMapUri { get; } = "ms-appx:///Assets/Sprites/Dev/spr_point.jpg";

        private float originalX = 0;
        private float originalY = 0;
        private float valueY = 0;
        private float valueX = 0;

        public async void LoadImageResourceAsync(CanvasDevice device)
        {
            BitMap = await CanvasBitmap.LoadAsync(device, new Uri(BitMapUri));
        }

        public void DrawSelf(CanvasDrawingSession ds)
        {
            if (originalX == 0 && originalX == 0)
            {
                originalX = X;
                originalY = Y;
            }

            Shake(60, 0.01f, 0.015f);

            if (BitMap != null)
            {
                ds.DrawImage(BitMap, X - 4, Y - 4);
            }
        }

        private void Shake(float threshold, float incrementX, float incrementY)
        {   // Pins point to intersection of two moving lines to make a shake effect
            Vector3 f1 = new Vector3(); // X = A, Y = B, Z = C
            Vector3 f2 = new Vector3();


            valueY += incrementY;
            valueX += incrementX;

            f1.X = 0;
            f1.Y = 1;
            f1.Z = originalY + (float)(threshold * Sin(valueY));

            f2.X = 1;
            f2.Y = 0;
            f2.Z = originalX + (float)(threshold * Sin(valueX));

            float delta = f1.X * f2.Y - f2.X * f1.Y;

            float x = (f2.Y * f1.Z - f1.Y * f2.Z) / delta;
            float y = (f1.X * f2.Z - f2.X * f1.Z) / delta;

            X = x;
            Y = y;
        }
    }
}

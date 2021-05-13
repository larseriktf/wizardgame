using Microsoft.Graphics.Canvas;
using System.Numerics;
using WizardGame.App.Classes.Graphics;
using WizardGame.App.Interfaces;
using static System.Math;

namespace WizardGame.App.Classes.Entities.Dev
{
    public class Target : Entity, IDrawable
    {
        

        private float originalX = 0;
        private float originalY = 0;
        private float valueY = 0;
        private float valueX = 0;

        public Target()
        {
            bitmap = ImageLoader.GetBitMap("bitmap_target");
        }

        public void Update()
        {
            if (originalX == 0 && originalX == 0)
            {
                originalX = X;
                originalY = Y;
            }

            Shake(60, 0.01f, 0.015f);
        }

        public void Draw(CanvasDrawingSession ds)
        {
            ds.DrawImage(bitmap, X - 4, Y - 4);
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

using Microsoft.Graphics.Canvas;
using System.Numerics;
using System.Timers;
using WizardGame.App.Classes.Graphics;
using WizardGame.App.Interfaces;
using static System.Math;
using static WizardGame.App.Classes.EntityManager;
using static WizardGame.App.Classes.RandomProvider;

namespace WizardGame.App.Classes.Entities.ParticleEffects
{
    public class DustCloud : Particle, IDrawable
    {
        private double angle = 0;
        private float startAngle = (float)(Rnd.NextDouble() * 2 * PI);
        private float alphaValue = 2;
        public double Angle
        {
            get
            {
                return angle;
            }
            set
            {
                angle = value;
            }
        }
        private double speed = Rnd.NextDouble() * (5.5 - 3.5) + 3.5; // random.NextDouble() * (max - min) + min
        public DustCloud()
        {
            spriteSheet = ImageLoader.GetSpriteSheet("sheet_dust_particle");
            Width = 5;
            Height = 5;
            ImageX = Rnd.Next(0, 3);

            double scale = Rnd.NextDouble() * (1.2 - 0.8) + 0.8;
            XScale = (float)scale;
            YScale = (float)scale;
        }

        public void Update()
        {
            HandleState();
            CalculateMovement();
            UpdateCollisions();
            OffsetAndScale();
        }

        public void Draw(CanvasDrawingSession ds)
        {
            
            alphaValue -= 0.05f;

            if (alphaValue <= 1)
            {
                Alpha = alphaValue;
            }

            using (var spriteBatch = ds.CreateSpriteBatch())
            {
                spriteSheet.DrawSpriteExt(
                    spriteBatch,
                    new Vector2(OffsetX, OffsetY),
                    new Vector2(ImageX, ImageY),
                    new Vector4(Red, Green, Blue, Alpha),
                    startAngle,
                    new Vector2(OffsetXScale, OffsetYScale),
                    0);
            }
        }

        private void CalculateMovement()
        {
            // Calculate movement
            ControlAngle(ref angle);

            speed = speed / 1.15;

            hsp = (float)(speed * Cos(angle));
            vsp = (float)(speed * Sin(angle));

            X += hsp;
            Y += vsp;
        }

        private void ControlAngle(ref double angle)
        {   // Contain Angle within its bounds
            if (angle >= 2 * PI)
            {
                angle -= 2 * PI;
            }
            else if (angle < 0)
            {
                angle += 2 * PI;
            }
        }

        private void HandleState()
        {
            switch (state)
            {
                case 0:
                    break;
                default:
                    RemoveEntity(this);
                    break;
            }

        }

        public static void Spawner(float x, float y, int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                EntityManager.AddEntity("layer_particles", new DustCloud()
                {
                    X = x,
                    Y = y,
                    Angle = (2 * PI) / amount * i + Rnd.NextDouble()
                });

            }
        }
    }
}

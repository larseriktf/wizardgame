using Microsoft.Graphics.Canvas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using WizardGame.App.Classes.Graphics;
using WizardGame.App.Interfaces;
using static WizardGame.App.Classes.EntityManager;
using static System.Math;

namespace WizardGame.App.Classes.Entities.ParticleEffects
{
    public class IceParticle : Particle, IDrawable
    {
        private readonly SpriteSheet spriteSheet;
        private readonly Random rnd = new Random();
        private double speed = 0;
        private double angle = 0;
        public IceParticle()
        {
            spriteSheet = ImageLoader.GetSpriteSheet("sheet_ice_particle");
            Width = 5;
            Height = 5;
            ImageX = rnd.Next(0, 3);
            speed = rnd.NextDouble() * 6;
            angle = rnd.NextDouble() * 2 * PI;

            fadeOutTimer = new Timer(fadeOutStartTime);
            fadeOutTimer.Elapsed += delegate (object source, ElapsedEventArgs e)
            {
                RemoveEntity(this);
            };
            fadeOutTimer.Start();
        }

        public void Draw(CanvasDrawingSession ds)
        {
            ControlAngle(ref angle);

            hsp = (float)(speed * Cos(angle));
            vsp = (float)(speed * Sin(angle));

            X += hsp;
            Y += vsp;

            using (var spriteBatch = ds.CreateSpriteBatch())
            {
                spriteSheet.DrawSpriteExt(
                    spriteBatch,
                    new Vector2(X, Y),
                    new Vector2(ImageX, ImageY),
                    new Vector4(Red, Green, Blue, Alpha),
                    0,
                    new Vector2(XScale, YScale),
                    0);
            }
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
    }
}

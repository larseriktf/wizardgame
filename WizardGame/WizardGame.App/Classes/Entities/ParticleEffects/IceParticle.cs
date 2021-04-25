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
        private readonly Random random = new Random();
        private readonly float grv = 0.2f;
        private float hspeed = 0;
        private float vspeed = 0;
        public IceParticle()
        {
            spriteSheet = ImageLoader.GetSpriteSheet("sheet_ice_particle");
            Width = 5;
            Height = 5;
            ImageX = random.Next(0, 3);
            hspeed = (float)random.NextDouble() * 4 * (random.Next(0, 2) == 1 ? 1 : -1);
            vspeed = (float)random.NextDouble() * 4 * (random.Next(0, 2) == 1 ? 1 : -1);

            fadeOutTimer = new Timer(fadeOutStartTime);
            fadeOutTimer.Elapsed += delegate (object source, ElapsedEventArgs e)
            {
                RemoveEntity(this);
            };
            fadeOutTimer.Start();
        }

        public void Draw(CanvasDrawingSession ds)
        {
            vspeed += grv;

            hsp = hspeed;
            vsp = vspeed;

            X += hsp;
            Y += vsp;

            UpdateCollisions();

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

        public static void Spawner(float x, float y, int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                EntityManager.Entities.Add(new IceParticle()
                {
                    X = x,
                    Y = y
                });
            }
        }
    }
}

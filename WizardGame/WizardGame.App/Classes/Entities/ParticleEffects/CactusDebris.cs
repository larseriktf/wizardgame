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
    class CactusDebris : Particle, IDrawable
    {
        private readonly float grv = 0.3f;
        private float hspeed = 0;
        private float vspeed = 0;
        private int state = 0;
        public CactusDebris()
        {
            spriteSheet = ImageLoader.GetSpriteSheet("sheet_cactus_debris");
            Width = 5;
            Height = 5;
            ImageX = Rnd.Next(0, 3);
            hspeed = (float)Rnd.NextDouble() * 4 * (Rnd.Next(0, 2) == 1 ? 1 : -1);
            vspeed = (float)Rnd.NextDouble() * 4 * (Rnd.Next(0, 2) == 1 ? 1 : -1);

            fadeOutTimer = new Timer(fadeOutStartTime);
            fadeOutTimer.Elapsed += delegate (object source, ElapsedEventArgs e)
            {
                state++;
            };
            fadeOutTimer.Start();
        }

        public void Update()
        {
            HandleState();

            vspeed += grv;

            hsp = hspeed;
            vsp = vspeed;

            UpdateCollisions();
        }

        public void Draw(CanvasDrawingSession ds)
        {
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
                EntityManager.AddEntity("layer_particles", new CactusDebris()
                {
                    X = x,
                    Y = y
                });
            }
        }
    }
}

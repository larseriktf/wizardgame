using Microsoft.Graphics.Canvas;
using System.Numerics;
using System.Timers;
using WizardGame.App.Classes.Graphics;
using WizardGame.App.Interfaces;
using static WizardGame.App.Classes.EntityManager;
using static WizardGame.App.Classes.RandomProvider;

namespace WizardGame.App.Classes.Entities.ParticleEffects
{
    class IceCrystals : Particle, IDrawable
    {
        private float hspeed = 0;
        private float vspeed = 0;
        public IceCrystals() : base(Rnd.Next(200, 500))
        {
            spriteSheet = ImageLoader.GetSpriteSheet("sheet_ice_crystals");
            Width = 5;
            Height = 5;
            ImageX = Rnd.Next(0, 3);
            hspeed = (float)(Rnd.NextDouble() * 0.5 * (Rnd.Next(0, 2) == 1 ? 1 : -1));
            vspeed = (float)(Rnd.NextDouble() * 0.5 * (Rnd.Next(0, 2) == 1 ? 1 : -1));
        }

        public void Update()
        {
            HandleState();

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
                EntityManager.AddEntity("layer_particles", new IceCrystals()
                {
                    X = x,
                    Y = y
                });
            }
        }
    }
}

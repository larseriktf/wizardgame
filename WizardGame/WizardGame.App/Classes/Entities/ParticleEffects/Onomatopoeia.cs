using Microsoft.Graphics.Canvas;
using System.Numerics;
using System.Timers;
using WizardGame.App.Classes.Graphics;
using WizardGame.App.Interfaces;
using static WizardGame.App.Classes.EntityManager;

namespace WizardGame.App.Classes.Entities.ParticleEffects
{
    public class Onomatopoeia : Particle, IDrawable
    {
        private readonly SpriteSheet spriteSheet;
        public Onomatopoeia()
        {
            spriteSheet = ImageLoader.GetSpriteSheet("sheet_onomatopoeia_particle");

            fadeOutTimer = new Timer(fadeOutStartTime);
            fadeOutTimer.Elapsed += delegate (object source, ElapsedEventArgs e)
            {
                state++;
            };
            fadeOutTimer.Start();
        }

        public void Draw(CanvasDrawingSession ds)
        {
            HandleState();

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
                EntityManager.Entities.Add(new IceShard()
                {
                    X = x,
                    Y = y
                });
            }
        }
    }
}

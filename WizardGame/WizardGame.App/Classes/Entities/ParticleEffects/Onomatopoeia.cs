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
        private float alphaValue = 2;
        public Onomatopoeia()
        {
            spriteSheet = ImageLoader.GetSpriteSheet("sheet_onomatopoeia_particle");
        }

        public void Update()
        {
            HandleState();
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

        public static void Spawner(float x, float y, int imageX)
        {

            EntityManager.AddEntity("layer_particles", new Onomatopoeia()
            {
                X = x,
                Y = y,
                ImageX = imageX
            });
        }
    }
}

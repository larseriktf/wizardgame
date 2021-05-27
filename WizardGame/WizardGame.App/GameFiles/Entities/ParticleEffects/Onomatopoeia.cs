using Microsoft.Graphics.Canvas;
using System.Numerics;
using WizardGame.App.GameFiles.Graphics;
using WizardGame.App.Interfaces;
using static WizardGame.App.GameFiles.EntityManager;

namespace WizardGame.App.GameFiles.Entities.ParticleEffects
{
    public class Onomatopoeia : Particle, IDrawable
    {
        private float alphaValue = 2;
        public Onomatopoeia(float x, float y) : base(x, y)
        {
            spriteSheet = ImageLoader.GetSpriteSheet("sheet_onomatopoeia_particle");
        }

        public void Update()
        {
            HandleState();
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
                    0,
                    new Vector2(OffsetXScale, OffsetYScale),
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

            EntityManager.AddEntity("layer_particles", new Onomatopoeia(x, y)
            {
                ImageX = imageX
            });
        }
    }
}

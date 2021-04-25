using Microsoft.Graphics.Canvas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using WizardGame.App.Classes.Graphics;
using WizardGame.App.Interfaces;

namespace WizardGame.App.Classes.Entities.ParticleEffects
{
    public class IceParticle : Particle, IDrawable
    {
        private readonly SpriteSheet spriteSheet;
        private readonly Random rnd = new Random();
        public IceParticle()
        {
            spriteSheet = ImageLoader.GetSpriteSheet("sheet_ice_particle");
            Width = 5;
            Height = 5;
            ImageX = rnd.Next(0, 3);
        }

        public void Draw(CanvasDrawingSession ds)
        {
            Y += 0.2f;

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
    }
}

using Microsoft.Graphics.Canvas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using WizardGame.App.Classes.Graphics;
using WizardGame.App.Interfaces;

namespace WizardGame.App.Classes.Entities.Spells
{
    public class IceSpell : Spell, IDrawable
    {
        private readonly SpriteSheet spriteSheet;

        public IceSpell()
        {
            ImageLoader.SpriteSheets.TryGetValue("sheet_ice_spell", out spriteSheet);
            X = 600;
            Y = 500;
            Width = 96;
            Height = 48;
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

            ds.DrawRectangle(X - Width / 2, Y - Height / 2, Width, Height, Colors.Yellow);
        }
    }
}

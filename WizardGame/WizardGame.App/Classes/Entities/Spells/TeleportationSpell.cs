using Microsoft.Graphics.Canvas;
using System.Numerics;
using Windows.UI;
using WizardGame.App.Classes.Graphics;
using WizardGame.App.Interfaces;

namespace WizardGame.App.Classes.Entities.Spells
{
    public class TeleportationSpell : Spell, IDrawable
    {

        public TeleportationSpell()
        {
            ImageLoader.SpriteSheets.TryGetValue("sheet_ice_spell", out spriteSheet);
            Width = 96;
            Height = 48;
            speed = 20;
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



        private void UpdateMovement()
        {

        }
    }
}

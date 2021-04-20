using Microsoft.Graphics.Canvas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using WizardGame.App.Interfaces;

namespace WizardGame.App.Classes.Entities.Spells
{
    public class IceSpell : Spell, IDrawable
    {
        public string BitMapUri { get; } = "ms-appx:///Assets/Sprites/Spells/spr_spell_ice.png";
        public SpriteSheet Sprite { get; set; } = null;
        public readonly int spriteWidth = 96;
        public readonly int spriteHeight = 48;

        public IceSpell()
        {
            X = 600;
            Y = 500;
            Width = 96;
            Height = 48;
        }

        public async void LoadImageResourceAsync(CanvasDevice device)
        {
            Sprite = await SpriteSheet.LoadSpriteSheetAsync(device, BitMapUri, new Vector2(spriteWidth, spriteHeight));
        }
        public void Draw(CanvasDrawingSession ds)
        {
            ds.DrawText("Hello!", X, Y, Colors.Green);
            using (var spriteBatch = ds.CreateSpriteBatch())
            {
                if (Sprite != null)
                {
                    Sprite.DrawSpriteExt(
                        spriteBatch,
                        new Vector2(X, Y),
                        new Vector2(ImageX, ImageY),
                        new Vector4(Red, Green, Blue, Alpha),
                        0,
                        new Vector2(XScale, YScale),
                        0);
                }
                else
                {
                    LoadImageResourceAsync(ds.Device);

                }
            }

            ds.DrawRectangle(X - Width / 2, Y - Height / 2, Width, Height, Colors.Yellow);
        }
    }
}

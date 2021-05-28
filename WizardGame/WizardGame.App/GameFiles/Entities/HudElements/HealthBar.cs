using Microsoft.Graphics.Canvas;
using System.Numerics;
using Windows.UI;
using WizardGame.App.GameFiles.Entities.Player;
using WizardGame.App.GameFiles.Graphics;
using WizardGame.App.Interfaces;

namespace WizardGame.App.GameFiles.Entities.HudElements
{
    public class HealthBar : Entity, IDrawable
    {
        public HealthBar() : base(384, 64)
        {
            spriteSheet = ImageLoader.GetSpriteSheet("sheet_health_bar");
        }

        public void Update()
        {
            OffsetAndScale();
        }

        public void Draw(CanvasDrawingSession ds)
        {
            using (var spriteBatch = ds.CreateSpriteBatch())
            {
                // Draw glass background
                spriteSheet.DrawSpriteExt(
                    spriteBatch,
                    new Vector2(OffsetX, OffsetY),
                    new Vector2(ImageX, 2),
                    new Vector4(Red, Green, Blue, Alpha),
                    0,
                    new Vector2(OffsetXScale, OffsetYScale),
                    0);

                // Draw cork
                spriteSheet.DrawSpriteExt(
                    spriteBatch,
                    new Vector2(OffsetX, OffsetY),
                    new Vector2(ImageX, 3),
                    new Vector4(Red, Green, Blue, Alpha),
                    0,
                    new Vector2(OffsetXScale, OffsetYScale),
                    0);

                // Draw liquid
                spriteSheet.DrawSpriteExt(
                    spriteBatch,
                    new Vector2(OffsetX, OffsetY),
                    new Vector2(ImageX, 1),
                    new Vector4(
                        Red + (Ghost.HP - 100) / (0 - 100),
                        Green, Blue, Alpha),
                    0,
                    new Vector2(OffsetXScale, OffsetYScale),
                    0);

                // Draw glass foreground
                spriteSheet.DrawSpriteExt(
                    spriteBatch,
                    new Vector2(OffsetX, OffsetY),
                    new Vector2(ImageX, 0),
                    new Vector4(Red, Green, Blue, Alpha),
                    0,
                    new Vector2(OffsetXScale, OffsetYScale),
                    0);
            }

            ds.FillCircle(
                OffsetX - OffsetWidth / 2 - 165,
                OffsetY - OffsetHeight / 2,
                20,
                Colors.Black);

            ds.DrawText("" + Ghost.HP,
            OffsetX - 165, OffsetY,
            Colors.Magenta, ApplicationSettings.standardFormat);
        }
    }
}

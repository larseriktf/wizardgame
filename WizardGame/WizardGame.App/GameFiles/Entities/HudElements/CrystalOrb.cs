using Microsoft.Graphics.Canvas;
using System.Numerics;
using Windows.UI;
using WizardGame.App.GameFiles.Graphics;
using WizardGame.App.Interfaces;

namespace WizardGame.App.GameFiles.Entities.HudElements
{
    class CrystalOrb : Entity, IDrawable
    {
        public CrystalOrb() : base(1744, 64, 96, 96)
        {
            spriteSheet = ImageLoader.GetSpriteSheet("sheet_crystal_orb");
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
                    new Vector2(ImageX, ImageY),
                    new Vector4(Red, Green, Blue, Alpha),
                    0,
                    new Vector2(OffsetXScale, OffsetYScale),
                    0);
            }


            ds.DrawText("" + GameManager.Wave,
                OffsetX, OffsetY,
                Colors.Magenta, ApplicationSettings.standardFormat);
        }
    }
}

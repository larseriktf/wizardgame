using Microsoft.Graphics.Canvas;
using System.Numerics;
using WizardGame.App.GameFiles.Entities;
using WizardGame.App.GameFiles.Graphics;
using WizardGame.App.Interfaces;

namespace WizardGame.App.GameFiles.MapMaker
{
    public class LevelBackground : Entity, IDrawable
    {
        public LevelBackground(int imageX, int imageY) : base(1920 / 2, 1152 / 2)
        {
            spriteSheet = ImageLoader.GetSpriteSheet("sheet_levels");
            ImageX = imageX;
            ImageY = imageY;
        }

        public void Update()
        {
            OffsetAndScale();
        }

        public void Draw(CanvasDrawingSession ds)
        {
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
    }
}

using Microsoft.Graphics.Canvas;
using System;
using System.Numerics;
using System.Threading.Tasks;
using Windows.Foundation;

namespace WizardGame.App.GameFiles.Graphics
{
    /// <summary>Class that handles SpriteSheet images and includes methods for extracting and modifying sprites</summary>
    public class SpriteSheet
    {
        private readonly CanvasBitmap bitmap;

        public Vector2 SpriteSize { get; set; }

        public SpriteSheet(CanvasBitmap bitmap, Vector2 spriteSize)
        {
            this.bitmap = bitmap;
            SpriteSize = spriteSize;
        }


        public static async Task<SpriteSheet> LoadSpriteSheetAsync(CanvasDevice device, string fileName, Vector2 spriteSize)
        {
            return new SpriteSheet(await CanvasBitmap.LoadAsync(device, new Uri(fileName)), spriteSize);
        }

        public void DrawSprite(CanvasSpriteBatch spriteBatch, Vector2 pos, Vector2 imagePos)
        {
            spriteBatch.DrawFromSpriteSheet(
                bitmap,
                new Rect(pos.X, pos.Y, SpriteSize.X, SpriteSize.Y),
                new Rect(imagePos.X * SpriteSize.X, imagePos.Y * SpriteSize.Y, SpriteSize.X, SpriteSize.Y));
        }

        public void DrawSpriteExt(CanvasSpriteBatch spriteBatch,
            Vector2 pos, Vector2 imagePos, Vector4 rgba, float rotation, Vector2 scale, CanvasSpriteFlip flip)
        {   // Draw Sprite Extended
            spriteBatch.DrawFromSpriteSheet(
                bitmap,
                pos,
                new Rect(imagePos.X * SpriteSize.X, imagePos.Y * SpriteSize.Y, SpriteSize.X, SpriteSize.Y),
                rgba,                                                   // Tint -> rgba [0 to 1]
                new Vector2(SpriteSize.X / 2, SpriteSize.Y / 2),        // Origin, now centered in the middle
                rotation,                                               // Rotation in radians
                scale,                                                  // Scale (default: 1, 1)
                flip);                                                  // CanvasSpriteFlip -> 0, 1, 2 or 3
        }
    }
}

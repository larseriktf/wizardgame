using Microsoft.Graphics.Canvas;
using WizardGame.App.Classes.Graphics;
using WizardGame.App.Views;

namespace WizardGame.App.Classes.Entities
{
    public abstract class Entity
    {
        protected SpriteSheet spriteSheet;
        protected CanvasBitmap bitmap;
        public string Layer { get; set; } = "baseLayer";
        public float X { get; set; } = 0;
        public float Y { get; set; } = 0;
        public float OffsetX { get; set; } = 0;
        public float OffsetY { get; set; } = 0;

        public int Width { get; set; } = 0;
        public int Height { get; set; } = 0;
        public int OffsetWidth { get; set; } = 0;
        public int OffsetHeight { get; set; } = 0;

        public float XScale { get; set; } = 1f;
        public float YScale { get; set; } = 1f;
        public float OffsetXScale { get; set; } = 1f;
        public float OffsetYScale { get; set; } = 1f;

        public int ImageX { get; set; } = 0;
        public int ImageY { get; set; } = 0;

        public float Red { get; set; } = 1f;
        public float Green { get; set; } = 1f;
        public float Blue { get; set; } = 1f;
        public float Alpha { get; set; } = 1f;

        // @TODO: Remove later, used for testing
        public static int ScreenWidth { get; set; } = 1920;
        public static int StandardScreenWidth { get; set; } = 1920;
        public static int StandardScreenHeight { get; set; } = 1080;

        protected void OffsetAndScale()
        {
            OffsetX = X * ScreenWidth / StandardScreenWidth;
            OffsetY = Y * ScreenWidth / StandardScreenWidth;
            OffsetWidth = Width * ScreenWidth / StandardScreenWidth;
            OffsetHeight = Height * ScreenWidth / StandardScreenWidth;
            OffsetXScale = XScale * ScreenWidth / StandardScreenWidth;
            OffsetYScale = YScale * ScreenWidth / StandardScreenWidth;
        }
    }
}

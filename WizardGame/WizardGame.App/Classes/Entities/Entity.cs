using Microsoft.Graphics.Canvas;
using Windows.Foundation;
using Windows.Graphics.Display;
using Windows.UI.ViewManagement;
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

        protected void OffsetAndScale()
        {
            OffsetX = (float)(X * Screen.Width / Screen.StandardWidth);
            OffsetY = (float)(Y * Screen.Width / Screen.StandardWidth);
            OffsetWidth = (int)(Width * Screen.Width / Screen.StandardWidth);
            OffsetHeight = (int)(Height * Screen.Width / Screen.StandardWidth);
            OffsetXScale = (float)(XScale * Screen.Width / Screen.StandardWidth);
            OffsetYScale = (float)(YScale * Screen.Width / Screen.StandardWidth);
        }
    }
}

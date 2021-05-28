using Microsoft.Graphics.Canvas;
using WizardGame.App.GameFiles.Graphics;

namespace WizardGame.App.GameFiles.Entities
{
    /// <summary>Base class for all game entities. Includes essential properties and scale and offset methods to contain the game within the window</summary>
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

        private float scaling = (float)(Screen.Width / Screen.StandardWidth);

        public Entity(
            float x, float y,
            int width = 0, int height = 0,
            float xScale = 1, float yScale = 1)
        {
            X = x;
            Y = y;
            OffsetX = x * scaling;
            OffsetY = y * scaling;

            Width = width;
            Height = height;
            OffsetWidth = (int)(width * scaling);
            OffsetHeight = (int)(height * scaling);

            XScale = xScale;
            YScale = yScale;
            OffsetXScale = xScale * scaling;
            OffsetYScale = yScale * scaling;
        }

        protected void OffsetAndScale()
        {
            scaling = (float)(Screen.Width / Screen.StandardWidth);

            OffsetX = X * scaling;
            OffsetY = Y * scaling;
            OffsetWidth = (int)(Width * scaling);
            OffsetHeight = (int)(Height * scaling);
            OffsetXScale = XScale * scaling;
            OffsetYScale = YScale * scaling;
        }
    }
}

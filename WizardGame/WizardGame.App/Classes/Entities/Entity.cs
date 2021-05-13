using Microsoft.Graphics.Canvas;
using WizardGame.App.Classes.Graphics;

namespace WizardGame.App.Classes.Entities
{
    public abstract class Entity
    {
        protected SpriteSheet spriteSheet;
        protected CanvasBitmap bitmap;
        public string Layer { get; set; } = "baseLayer";
        public float X { get; set; } = 0;
        public float Y { get; set; } = 0;
        public int Width { get; set; } = 0;
        public int Height { get; set; } = 0;

        public int ImageX { get; set; } = 0;
        public int ImageY { get; set; } = 0;
        public float XScale { get; set; } = 1f;
        public float YScale { get; set; } = 1f;

        public float Red { get; set; } = 1f;
        public float Green { get; set; } = 1f;
        public float Blue { get; set; } = 1f;
        public float Alpha { get; set; } = 1f;
    }
}

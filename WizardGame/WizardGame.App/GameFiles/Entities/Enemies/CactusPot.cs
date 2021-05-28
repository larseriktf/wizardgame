using Microsoft.Graphics.Canvas;
using System.Numerics;
using WizardGame.App.GameFiles.Entities.Dev;
using WizardGame.App.GameFiles.Graphics;
using WizardGame.App.Interfaces;
using static System.Math;
using static WizardGame.App.GameFiles.EntityManager;

namespace WizardGame.App.GameFiles.Entities.Enemies
{
    class CactusPot : PhysicsObject, IDrawable
    {
        private double direction = PI / 2;

        // Cactus placing values
        private bool cactusPlaced = false;
        private bool allowPlacement = true;

        public CactusPot(float x, float y) : base(x, y, 64, 64)
        {
            spriteSheet = ImageLoader.GetSpriteSheet("sheet_cactus_enemy");
        }

        public void Update()
        {
            PlaceCactus();

            if (Sign(hsp) != 0)
            {
                XScale = Sign(hsp);
            }

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

        private void PlaceCactus()
        {
            // If allowed to allowed to place and haven't placed yet
            if (cactusPlaced == false && allowPlacement == true)
            {
                // @TODO
                // Calculate newX and newY
                float newX = X + 0;
                float newY = Y - 64;

                // if area next of cactus relative to angle is available, place new cactus
                if (!IsColliding(newX, newY, Width, Height, typeof(Solid)))
                {
                    AddEntity("layer1", new Cactus(newX, newY)
                    {
                        Direction = direction
                    });
                }

                cactusPlaced = true;
            }
        }
    }
}

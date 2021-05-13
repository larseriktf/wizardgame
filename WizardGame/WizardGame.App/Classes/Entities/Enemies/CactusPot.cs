using Microsoft.Graphics.Canvas;
using System;
using System.Numerics;
using WizardGame.App.Classes.Entities.Dev;
using WizardGame.App.Classes.Entities.Enemies;
using WizardGame.App.Classes.Entities.HudElements;
using WizardGame.App.Classes.Entities.ParticleEffects;
using WizardGame.App.Classes.Entities.Spells;
using WizardGame.App.Classes.Graphics;
using WizardGame.App.Interfaces;
using WizardGame.App.Views;
using static System.Math;
using static WizardGame.App.Classes.EntityManager;
using static WizardGame.App.Classes.Input.KeyBoard;

namespace WizardGame.App.Classes.Entities.Enemies
{
    class CactusPot : PhysicsObject, IDrawable
    {
        private double direction = PI / 2;

        // Cactus placing values
        private bool cactusPlaced = false;
        private bool allowPlacement = true;

        public CactusPot()
        {
            spriteSheet = ImageLoader.GetSpriteSheet("sheet_cactus_enemy");
            Width = 64;
            Height = 64;
        }

        public void Update()
        {
            PlaceCactus();

            if (Sign(hsp) != 0)
            {
                XScale = Sign(hsp);
            }
        }

        public void Draw(CanvasDrawingSession ds)
        {
            using (var spriteBatch = ds.CreateSpriteBatch())
            {
                spriteSheet.DrawSpriteExt(
                    spriteBatch,
                    new Vector2(X, Y),
                    new Vector2(ImageX, ImageY),
                    new Vector4(Red, Green, Blue, Alpha),
                    0,
                    new Vector2(XScale, YScale),
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
                    AddEntity("layer1", new Cactus()
                    {
                        X = newX,
                        Y = newY,
                        Direction = direction
                    });
                }

                cactusPlaced = true;
            }
        }
    }
}

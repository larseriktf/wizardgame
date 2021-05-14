using Microsoft.Graphics.Canvas;
using System;
using System.Numerics;
using System.Timers;
using Windows.UI;
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
using static WizardGame.App.Classes.RandomProvider;

namespace WizardGame.App.Classes.Entities.Enemies
{
    class Cactus : Enemy, IDrawable
    {
        private double direction = 0;
        public double Direction
        {
            get
            {
                return direction;
            }
            set
            {
                direction = value;
            }
        }

        // Sprite values
        private int sprTop = 4;
        private int sprMiddle = 1;
        private int sprBottom = 5;
        private int sprConnect = 3;
        private int sprCurrent = 3;

        // Cactus placing values
        private bool allowPlacement = false;
        private Timer placementTimer;
        private int placeTime = 1000;

        // @TODO: Remove this
        private bool colliding = false;

        public Cactus() 
        {
            spriteSheet = ImageLoader.GetSpriteSheet("sheet_cactus_enemy");
            Width = 64;
            Height = 64;

            placementTimer = new Timer(placeTime);
            placementTimer.Elapsed += delegate (object source, ElapsedEventArgs e)
            {
                allowPlacement = true;
            };
            placementTimer.Start();
        }

        public void Update()
        {
            PlaceCactus();
            HandleSprites();
            OffsetAndScale();
        }

        public void Draw(CanvasDrawingSession ds)
        {
            using (var spriteBatch = ds.CreateSpriteBatch())
            {
                spriteSheet.DrawSpriteExt(
                    spriteBatch,
                    new Vector2(OffsetX, OffsetY),
                    new Vector2(sprCurrent, ImageY),
                    new Vector4(Red, Green, Blue, Alpha),
                    0,
                    new Vector2(OffsetXScale, OffsetYScale),
                    0);
            }

            ds.DrawText("Colliding: " + colliding, X + 64, Y, Colors.Blue);
        }

        private void PlaceCactus()
        {
            // If allowed to allowed to place and haven't placed yet
            if (allowPlacement == true)
            {
                // @TODO
                // Calculate newX and newY
                float newX = X + (float)(64 * Cos(direction));
                float newY = Y + (float)(64 * Sin(direction));

                // if area next of cactus relative to angle is available, place new cactus
                if (IsOverlapping(newX, newY, Width, Height, typeof(Solid))
                 || IsOverlapping(newX, newY, Width, Height, typeof(Cactus)))
                {
                    colliding = true;
                }
                if (!colliding)
                {
                    AddEntity("layer1", new Cactus()
                    {
                        X = newX,
                        Y = newY,
                        Direction = direction
                    });
                    CactusDebris.Spawner(newX, newY, Rnd.Next(5, 8));
                }
                allowPlacement = false;
            }
        }

        private void HandleSprites()
        {
            if (Sign(hsp) != 0)
            {
                XScale = Sign(hsp);
            }

            ControlAngle(ref direction);

            if (direction > (PI / 4) && direction < (3 * PI / 4)
             || direction > (5 * PI / 4) && direction < (2 * PI))
            {   // Direction is vertical
                sprTop = 4;
                sprMiddle = 1;
                sprBottom = 5;
            }
            else
            {   // Direction is horizontal
                sprTop = 6;
                sprMiddle = 2;
                sprBottom = 7;
            }

            // @TODO: Add logic to change sprCurrent, depending on placement of other cactus enemies


            sprCurrent = sprMiddle;
        }

        private void ControlAngle(ref double angle)
        {   // Contain Angle within its bounds
            if (angle >= 2 * PI)
            {
                angle -= 2 * PI;
            }
            else if (angle < 0)
            {
                angle += 2 * PI;
            }
        }

        protected override void Die()
        {
            CactusDebris.Spawner(X, Y, Rnd.Next(4, 7));
            RemoveEntity(this);
        }
    }
}

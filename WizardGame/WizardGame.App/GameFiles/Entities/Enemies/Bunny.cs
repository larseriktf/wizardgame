using Microsoft.Graphics.Canvas;
using System.Numerics;
using System.Timers;
using Windows.UI;
using WizardGame.App.GameFiles.Entities.Dev;
using WizardGame.App.GameFiles.Entities.Player;
using WizardGame.App.GameFiles.Graphics;
using WizardGame.App.Interfaces;
using static System.Math;
using static WizardGame.App.GameFiles.EntityManager;

namespace WizardGame.App.GameFiles.Entities.Enemies
{
    public class Bunny : Enemy, IDrawable
    {
        private readonly Timer animTimer;

        public Bunny(float x, float y) : base(x, y, 96, 96)
        {
            hp = 20;
            damage = 5;
            spriteSheet = ImageLoader.GetSpriteSheet("sheet_bunny");

            animTimer = new Timer(40);
            animTimer.Elapsed += delegate (object source, ElapsedEventArgs e)
            {   // Plays animation on timer tick
                PlayAnimation();
            };
            animTimer.Start();
        }

        public void Update()
        {
            UpdateMovement();

            if (Sign(hsp) != 0)
            {
                XScale = Sign(hsp);
            }

            ImageY = 1;
            OffsetAndScale();
            DamagePlayerOnCollision();
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

            //ds.DrawRectangle(
            //    OffsetX - OffsetWidth / 2,
            //    OffsetY - OffsetHeight / 2,
            //    OffsetWidth, OffsetHeight,
            //    Colors.Green);
        }

        private void PlayAnimation()
        {
            ImageX++;
            if (ImageX >= 13)
            {
                ImageX = 0;
            }
            else if (ImageX < 0)
            {
                ImageX = 12;
            }
            animTimer.Interval = (40);
        }

        private int moveDir = 1;
        private void UpdateMovement()
        {   // Calculate movement
            if (IsColliding(X + hsp, Y, Width, Height, typeof(Solid)))
            {
                moveDir = -moveDir;
            }

            hsp = moveDir * moveSpeed;
            vsp += Gravity;

            UpdateCollisions();
        }
    }
}

using Microsoft.Graphics.Canvas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Windows.UI;
using WizardGame.App.Classes.Entities.Dev;
using WizardGame.App.Classes.Graphics;
using WizardGame.App.Interfaces;
using static System.Math;

namespace WizardGame.App.Classes.Entities.Characters
{
    public class Bunny : Character, IDrawable
    {
        public int MoveSpeed { get; set; } = 3;

        private readonly SpriteSheet spriteSheet;
        private readonly float gravity = 0.5f;
        Timer animTimer;

        public Bunny()
        {
            spriteSheet = ImageLoader.GetSpriteSheet("sheet_bunny");
            Width = 96;
            Height = 96;
            HP = 8;

            animTimer = new Timer(40);
            animTimer.Elapsed += delegate (object source, ElapsedEventArgs e)
            {   // Plays animation on timer tick
                PlayAnimation();
            };
            animTimer.Start();
        }

        public void Draw(CanvasDrawingSession ds)
        {
            UpdateMovement();

            if (HP <= 0)
            {
                EntityManager.RemoveEntity(this);
            }

            if (Sign(hsp) != 0)
            {
                XScale = Sign(hsp);
            }

            ImageY = 1;

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

            ds.DrawRectangle(X - Width / 2, Y - Height / 2, Width, Height, Colors.Green);
            ds.DrawText("HP: " + HP, X, Y, Colors.Red);
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
            if (EntityManager.CheckCollision(X + hsp, Y, Width, Height, typeof(Solid)))
            {
                moveDir = -moveDir;
            }

            hsp = moveDir * MoveSpeed;
            vsp += gravity;

            UpdateCollisions();
        }
    }
}

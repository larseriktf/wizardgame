using Microsoft.Graphics.Canvas;
using System;
using System.Numerics;
using System.Timers;
using Windows.UI;
using WizardGame.App.Classes.Entities.Dev;
using WizardGame.App.Classes.Entities.ParticleEffects;
using WizardGame.App.Classes.Entities.Spells;
using WizardGame.App.Classes.Graphics;
using WizardGame.App.Interfaces;
using static System.Math;
using static WizardGame.App.Classes.EntityManager;
using static WizardGame.App.Classes.RandomProvider;

namespace WizardGame.App.Classes.Entities.Characters
{
    public class Bunny : Character, IDrawable
    {
        public int MoveSpeed { get; set; } = 3;

        private readonly SpriteSheet spriteSheet;
        private readonly float gravity = 0.5f;
        private readonly int maxHP = 8;
        private readonly Timer animTimer;

        public Bunny()
        {
            spriteSheet = ImageLoader.GetSpriteSheet("sheet_bunny");
            Width = 96;
            Height = 96;
            HP = maxHP;

            animTimer = new Timer(40);
            animTimer.Elapsed += delegate (object source, ElapsedEventArgs e)
            {   // Plays animation on timer tick
                PlayAnimation();
            };
            animTimer.Start();
        }

        public void Update()
        {
            UpdateAliveState();
            UpdateMovement();
            UpdateInvincibility();

            if (Sign(hsp) != 0)
            {
                XScale = Sign(hsp);
            }

            ImageY = 1;
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

            ds.DrawRectangle(X - Width / 2, Y - Height / 2, Width, Height, Colors.Green);
            ds.DrawText("HP: " + HP, X, Y, Colors.Red);
            ds.DrawText("Invincibility: " + Invincible, X, Y - 16, Colors.Green);
            ds.DrawText("Timer: " + invincibilityTimer.Interval, X, Y - 32, Colors.Blue);
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
            if (EntityManager.CheckCollisionMultiple(X + hsp, Y, Width, Height, typeof(Solid)))
            {
                moveDir = -moveDir;
            }

            hsp = moveDir * MoveSpeed;
            vsp += gravity;

            UpdateCollisions();
        }
    }
}

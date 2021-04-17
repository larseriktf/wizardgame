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
using WizardGame.App.Interfaces;
using static System.Math;

namespace WizardGame.App.Classes.Entities.Characters
{
    public class Bunny : Entity, IDrawable
    {
        public int MoveSpeed { get; set; } = 3;

        public string BitMapUri { get; } = "ms-appx:///Assets/Sprites/Entities/Bunny/spr_bunny.png";
        public SpriteSheet Sprite { get; set; } = null;
        public readonly int spriteWidth = 96;
        public readonly int spriteHeight = 96;

        private float vsp;
        private float hsp;
        private readonly float gravity = 0.5f;
        Timer animTimer;

        public Bunny()
        {
            Width = 96;
            Height = 96;

            animTimer = new Timer(40);
            animTimer.Elapsed += delegate (object source, ElapsedEventArgs e)
            {   // Plays animation on timer tick
                PlayAnimation();
            };
            animTimer.Start();
        }

        public async void LoadImageResourceAsync(CanvasDevice device)
        {
            Sprite = await SpriteSheet.LoadSpriteSheetAsync(device, BitMapUri, new Vector2(spriteWidth, spriteHeight));
        }

        public void Draw(CanvasDrawingSession ds)
        {
            UpdateMovement();

            if (Sign(hsp) != 0)
            {
                XScale = Sign(hsp);
            }

            ImageY = 1;

            using (var spriteBatch = ds.CreateSpriteBatch())
            {
                if (Sprite != null)
                {
                    Sprite.DrawSpriteExt(
                    spriteBatch,
                    new Vector2(X, Y),
                    new Vector2(ImageX, ImageY),
                    new Vector4(Red, Green, Blue, Alpha),
                    0,
                    new Vector2(XScale, YScale),
                    0);
                }
            }

            ds.DrawRectangle(X - Width / 2, Y - Height / 2, Width, Height, Colors.Green);
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

            // Horizontal Collision
            if (EntityManager.CheckCollision(X + hsp, Y, Width, Height, typeof(Solid)))
            {
                while (!EntityManager.CheckCollision(X + Sign(hsp), Y, Width, Height, typeof(Solid)))
                {   // Move as close as possible to the entity
                    X += Sign(hsp);
                }
                hsp = 0;
            }

            // Update X
            X += hsp;

            // Horizontal Collision
            if (EntityManager.CheckCollision(X, Y + vsp, Width, Height, typeof(Solid)))
            {
                while (!EntityManager.CheckCollision(X, Y + Sign(vsp), Width, Height, typeof(Solid)))
                {   // Move as close as possible to the entity
                    Y += Sign(vsp);
                }
                vsp = 0;
            }

            // Update Y 
            Y += vsp;
        }
    }
}

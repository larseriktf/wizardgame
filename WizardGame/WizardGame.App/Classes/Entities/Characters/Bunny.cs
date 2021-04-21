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
    public class Bunny : Entity, IDrawable
    {
        public int MoveSpeed { get; set; } = 3;

        private SpriteSheet sprite = null;
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

        StringBuilder sb = new StringBuilder();

        public void Draw(CanvasDrawingSession ds)
        {
            //////
            // @TODO: Remove this later, this is just for testing purposes
            sb.Clear();
            foreach (Entity entity in EntityManager.Entities)
            {
                if (!entity.GetType().Equals(typeof(Solid)))
                {
                    sb.Append(entity.GetType() + "\n");
                }
            }
            CanvasDebugger.Debug(this, "Entities:\n" + sb.ToString());
            //////

            UpdateMovement();

            if (Sign(hsp) != 0)
            {
                XScale = Sign(hsp);
            }

            ImageY = 1;

            using (var spriteBatch = ds.CreateSpriteBatch())
            {
                if (sprite != null)
                {
                    sprite.DrawSpriteExt(
                        spriteBatch,
                        new Vector2(X, Y),
                        new Vector2(ImageX, ImageY),
                        new Vector4(Red, Green, Blue, Alpha),
                        0,
                        new Vector2(XScale, YScale),
                        0);
                }
                else
                {
                    sprite = ImageLoader.GetSpriteSheet("bunnySheet");
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

using Microsoft.Graphics.Canvas;
using System;
using System.Numerics;
using System.Timers;
using WizardGame.App.Classes.Entities.Dev;
using WizardGame.App.Classes.Graphics;
using WizardGame.App.Interfaces;
using static System.Math;
using WizardGame.App.Classes.Entities.Enemies;
using static WizardGame.App.Classes.RandomProvider;

namespace WizardGame.App.Classes.Entities.Enemies
{
    public class MagicCard : Enemy, IDrawable
    {
        private readonly double speed = 6 + Rnd.NextDouble() * 5;
        private double angle = Rnd.NextDouble() * 2 * Math.PI;
        private double decidedAngle = 0;
        private double targetAngle = 0;
        private double lagAngle = 0;
        private double turningSpeed = 0;

        private double dist = 0;
        private double amplifier = 0;

        private double threshold = 0;

        private readonly double wiggleRate = Rnd.NextDouble() * 0.1;
        private readonly double wiggleMultiplier = Rnd.NextDouble() * 0.025;
        private double wiggle = Rnd.NextDouble();

        // For debugging
        public static double Angle = 0;
        public static double TargetAngle = 0;
        public static double LagAngle = 0;

        Timer animTimer = null;
        Entity target;

        public MagicCard()
        {
            spriteSheet = ImageLoader.GetSpriteSheet("sheet_cardenemy");
        }

        public static void Spawner(float x, float y, int amount)
        {
            int min = -16;
            int max = 16;

            EntityManager.Entities.Add(new Target()
            {
                X = x,
                Y = y
            });

            for (int i = 0; i < amount; i++)
            {
                EntityManager.Entities.Add(new MagicCard()
                {
                    X = x + (float)Rnd.NextDouble() * (max - min) + min,
                    Y = y + (float)Rnd.NextDouble() * (max - min) + min
                });
            }
        }

        public void Update()
        {
            DetectStateChange();
            UpdateMovement();
            OffsetAndScale();
        }

        public void Draw(CanvasDrawingSession ds)
        {
            if (animTimer == null)
            {
                ImageX = Rnd.Next(0, 3);

                animTimer = new Timer(Rnd.Next(0, 2000));

                animTimer.Elapsed += delegate (object source, ElapsedEventArgs e)
                {   // Plays animation on timer tick
                    PlayAnimation();
                };
                animTimer.Start();
            }

            if (ImageX > 3)
            {
                ImageX = 0;
            }
            else if (ImageX < 0)
            {
                ImageX = 3;
            }

            if (ImageY > 3)
            {
                ImageY = 0;
            }
            else if (ImageY < 0)
            {
                ImageY = 3;
            }

            using (var spriteBatch = ds.CreateSpriteBatch())
            {
                spriteSheet.DrawSpriteExt(
                    spriteBatch,
                    new Vector2(OffsetX, OffsetY),
                    new Vector2(ImageX, ImageY),
                    new Vector4(Red, Green, Blue, Alpha),
                    (float)(angle + 0.5 * PI),
                    new Vector2(OffsetXScale, OffsetYScale),
                    0);
            }
        }

        private void DetectStateChange()
        {
            int prevState = 0;

            if (state != prevState)
            {
                PlayAnimation();
                prevState = state;
            }
        }

        private void PlayAnimation()
        {
            if (state == 0)
            {
                ImageY++;

                if (ImageY % 2 == 0)
                {   // Front / back of card
                    animTimer.Interval = Rnd.Next(1000, 3000);
                }
                else
                {   // Card is in transition
                    animTimer.Interval = 50;
                }
            }
            else if (state == 1)
            {
                if (ImageY != 2)
                {   // Back side or mid transition
                    ImageY++;
                    animTimer.Interval = 50;
                }
            }
        }

        private void UpdateMovement()
        {
            int minLength = 100;
            int maxLength = 150;

            Player player = (Player)EntityManager.NearestEntity(this, typeof(Player));

            if (EntityManager.DistanceBetweenEntities(this, player) < 500)
            {
                state = 1; // Chase
            }
            else
            {
                state = 0; // Guard
            }


            if (state == 0)
            {   // Guarding state
                target = EntityManager.NearestEntity(this, typeof(Target));
                targetAngle = EntityManager.AngleBetweenEntitiesInRadians(this, target);

                dist = EntityManager.DistanceBetweenEntities(this, target);

                lagAngle = angle;

                // Keep lag angle inside bounderies of [0, 2PI>
                if (lagAngle >= 2 * PI + targetAngle)
                {
                    lagAngle -= 2 * PI;
                }
                else if (lagAngle < 0 + targetAngle)
                {
                    lagAngle += 2 * PI;
                }

                // Range Threshold
                if (dist < maxLength)
                {
                    threshold = (PI * 0.5) * ((dist - minLength) / (maxLength - minLength));
                }

                if (lagAngle > PI + targetAngle && lagAngle < PI + targetAngle + threshold)
                {
                    lagAngle = PI + targetAngle + threshold;
                }
                if (lagAngle < PI + targetAngle && lagAngle > PI + targetAngle - threshold)
                {
                    lagAngle = PI + targetAngle - threshold;
                }

                decidedAngle = lagAngle;
                amplifier = 2.25;

            }
            else if (state == 1)
            {   // Chasing state
                target = EntityManager.NearestEntity(this, typeof(Player));
                targetAngle = EntityManager.AngleBetweenEntitiesInRadians(this, target);

                decidedAngle = angle;
                amplifier = 2.0;
            }

            // Keep angle inside bounderies of [0, 2PI>
            if (angle >= 2 * PI)
            {
                angle -= 2 * PI;
            }
            else if (angle < 0)
            {
                angle += 2 * PI;
            }

            turningSpeed = 0.05 * (EntityManager.CrossProductOfTwoVectors(
                                    new Vector2((float)Cos(decidedAngle), (float)Sin(decidedAngle)),
                                    new Vector2((float)Cos(targetAngle), (float)Sin(targetAngle))));
            wiggle += wiggleRate;

            angle += turningSpeed * amplifier + Sin(wiggle) * wiggleMultiplier;

            X += (float)(speed * Cos(angle));
            Y += (float)(speed * Sin(angle));
        }
    }
}

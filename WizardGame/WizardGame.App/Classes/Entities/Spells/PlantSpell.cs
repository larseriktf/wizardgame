using Microsoft.Graphics.Canvas;
using System.Numerics;
using Windows.UI;
using WizardGame.App.Classes.Entities.Enemies;
using WizardGame.App.Classes.Graphics;
using WizardGame.App.Interfaces;
using static System.Math;
using static WizardGame.App.Classes.EntityManager;
using static WizardGame.App.Classes.RandomProvider;

namespace WizardGame.App.Classes.Entities.Spells
{
    public class PlantSpell : Spell, IDrawable
    {
        private readonly float grv = 0.3f;
        private float rotation = 0;
        private float rotationIncrease = (float)(Rnd.NextDouble() * (0.3 - (-0.3)) + (-0.3));

        public PlantSpell()
        {
            ImageLoader.SpriteSheets.TryGetValue("sheet_plant_spell", out spriteSheet);
            Width = 48;
            Height = 48;
            vsp = -8;
        }

        public void Update()
        {
            UpdateMovement();
            HandleState();

            rotation += rotationIncrease;
            OffsetAndScale();
        }

        public void Draw(CanvasDrawingSession ds)
        {
            if (Sign(hsp) != 0)
            {
                XScale = Sign(hsp);
            }

            using (var spriteBatch = ds.CreateSpriteBatch())
            {
                spriteSheet.DrawSpriteExt(
                    spriteBatch,
                    new Vector2(OffsetX, OffsetY),
                    new Vector2(ImageX, ImageY),
                    new Vector4(Red, Green, Blue, Alpha),
                    rotation,
                    new Vector2(OffsetXScale, OffsetYScale),
                    0);
            }

            ds.DrawRectangle(OffsetX - OffsetWidth / 2, OffsetY - OffsetHeight / 2, OffsetWidth, OffsetHeight, Colors.Yellow);
        }

        private void HandleState()
        {
            switch (state)
            {
                case 0:
                    break;
                default:
                    RemoveEntity(this);
                    break;
            }
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

        private void UpdateMovement()
        {
            // Calculate movement
            hsp = (float)moveSpeed * Sign(direction);
            vsp += grv;

            X += hsp;
            Y += vsp;

            HandleCollisions();
        }


        private void HandleCollisions()
        {
            if (IsColliding(X, Y, Width, Height, typeof(Enemy)))
            {
                // If collided with character
                // Do damage to enemy character
                Enemy enemy = (Enemy)GetCollisionObject(X, Y, Width, Height, typeof(Enemy));
                if (enemy.Invincible == false)
                {
                    enemy.TakeDamage(damage);
                    state++;
                }
            }
        }
    }
}

using Microsoft.Graphics.Canvas;
using System.Numerics;
using Windows.UI;
using WizardGame.App.Classes.Entities.Dev;
using WizardGame.App.Classes.Entities.Enemies;
using WizardGame.App.Classes.Entities.ParticleEffects;
using WizardGame.App.Classes.Graphics;
using WizardGame.App.Interfaces;
using static System.Math;
using static WizardGame.App.Classes.EntityManager;
using static WizardGame.App.Classes.RandomProvider;

namespace WizardGame.App.Classes.Entities.Spells
{
    public class IceSpell : Spell, IDrawable
    {
        private int angleMod = 1;

        public IceSpell()
        {
            ImageLoader.SpriteSheets.TryGetValue("sheet_ice_spell", out spriteSheet);
            Width = 96;
            Height = 48;
            speed = 20;
        }

        public void Update()
        {
            UpdateMovement();
            HandleState();
        }

        public void Draw(CanvasDrawingSession ds)
        {
            if (direction < 3 * PI / 2 && direction >= PI / 2)
            {
                YScale = -1;
                angleMod = -1;
            }
            else
            {
                YScale = 1;
                angleMod = 1;
            }

            ImageX = state;

            using (var spriteBatch = ds.CreateSpriteBatch())
            {
                spriteSheet.DrawSpriteExt(
                    spriteBatch,
                    new Vector2(X, Y),
                    new Vector2(ImageX, ImageY),
                    new Vector4(Red, Green, Blue, Alpha),
                    (float)direction * angleMod,
                    new Vector2(XScale, YScale),
                    0);
            }

            ds.DrawRectangle(X - Width / 2, Y - Height / 2, Width, Height, Colors.Yellow);
        }

        private void HandleState()
        {
            switch (state)
            {
                case 0:
                    damage = 6;
                    break;
                case 1:
                    damage = 4;
                    break;
                case 2:
                    damage = 2;
                    break;
                default:
                    RemoveEntity(this);
                    break;
            }
        }

        private void UpdateMovement()
        {
            // Calculate movement
            ControlAngle(ref direction);

            hsp = (float)(speed * Cos(direction));
            vsp = (float)(speed * Sin(direction));

            X += hsp;
            Y += vsp;

            HandleCollisions();
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


        private void HandleCollisions()
        {
            if (CheckCollisionMultiple(X, Y, Width, Height, typeof(Enemy)))
            {
                // If collided with character
                // Do damage to enemy character
                Enemy enemy = (Enemy)GetCollisionObject(X, Y, Width, Height, typeof(Enemy));
                if (enemy.Invincible == false)
                {
                    enemy.TakeDamage(damage);
                    IceShard.Spawner(X, Y, Rnd.Next(4, 7));
                    state++;
                }
            }
            else if (CheckCollisionMultiple(X, Y, Width, Height, typeof(Solid)))
            {
                // If collided with wall
                IceShard.Spawner(X, Y, Rnd.Next(3, 5));
                state = 3;
            }
        }
    }
}

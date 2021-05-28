using Microsoft.Graphics.Canvas;
using System.Numerics;
using Windows.UI;
using WizardGame.App.GameFiles.Entities.Dev;
using WizardGame.App.GameFiles.Entities.Enemies;
using WizardGame.App.GameFiles.Entities.ParticleEffects;
using WizardGame.App.GameFiles.Graphics;
using WizardGame.App.Interfaces;
using static System.Math;
using static WizardGame.App.GameFiles.EntityManager;
using static WizardGame.App.Helpers.RandomProvider;

namespace WizardGame.App.GameFiles.Entities.Spells
{
    public class IceSpell : Spell, IDrawable
    {
        private int angleMod = 1;

        public IceSpell(float x, float y) : base(x, y, 96, 48)
        {
            ImageLoader.SpriteSheets.TryGetValue("sheet_ice_spell", out spriteSheet);
            moveSpeed = 32;
        }

        public void Update()
        {
            //MakeTrail();
            UpdateMovement();
            HandleState();

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

            OffsetAndScale();
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
                    (float)direction * angleMod,
                    new Vector2(OffsetXScale, OffsetYScale),
                    0);
            }
        }

        private void MakeTrail()
        {
            IceCrystals.Spawner(X, Y, 1);
        }

        private void HandleState()
        {
            switch (state)
            {
                case 0: damage = 6; break;
                case 1: damage = 4; break;
                case 2: damage = 2; break;
                default:
                    IceShard.Spawner(X, Y, Rnd.Next(3, 5));
                    RemoveEntity(this);
                    break;
            }
        }

        private void UpdateMovement()
        {
            // Calculate movement
            ControlAngle(ref direction);

            hsp = (float)(moveSpeed * Cos(direction));
            vsp = (float)(moveSpeed * Sin(direction));

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
            if (IsColliding(X, Y, Width, Height, typeof(Enemy)))
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
            else if (IsColliding(X, Y, Width, Height, typeof(Solid)))
            {
                state = 3;
            }
        }
    }
}

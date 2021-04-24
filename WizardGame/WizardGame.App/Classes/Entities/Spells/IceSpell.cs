using Microsoft.Graphics.Canvas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using WizardGame.App.Classes.Entities.Characters;
using WizardGame.App.Classes.Entities.Dev;
using WizardGame.App.Classes.Graphics;
using WizardGame.App.Interfaces;
using static System.Math;
using static WizardGame.App.Classes.EntityManager;

namespace WizardGame.App.Classes.Entities.Spells
{
    public class IceSpell : Spell, IDrawable
    {
        private readonly SpriteSheet spriteSheet;

        public int MyProperty { get; set; }

        public IceSpell()
        {
            ImageLoader.SpriteSheets.TryGetValue("sheet_ice_spell", out spriteSheet);
            collidables.AddRange(new Type[] {
                typeof(Solid),
                typeof(Bunny),
                typeof(CardEnemy)
            });
            Width = 96;
            Height = 48;
            Speed = 5;
        }

        public void Draw(CanvasDrawingSession ds)
        {
            UpdateMovement();

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

            ds.DrawRectangle(X - Width / 2, Y - Height / 2, Width, Height, Colors.Yellow);
        }

        private void UpdateMovement()
        {
            hsp = (float)(Speed * Cos(Angle));
            vsp = (float)(Speed * Sin(Angle));

            X += hsp;
            Y += vsp;

            HandleCollisions();
        }

        private void HandleCollisions()
        {
            if (CheckCollision(X, Y, Width, Height, typeof(Character)))
            {
                // If collided with character
                // Do damage to enemy character
                Character enemy = (Character)GetCollisionObject(X, Y, Width, Height, typeof(Character));
                enemy.HP -= Damage;

                RemoveEntity(this);
            }
            else if (CheckCollision(X, Y, Width, Height, typeof(Character)))
            {
                // If collided with wall
                RemoveEntity(this);
            }
        }
    }
}

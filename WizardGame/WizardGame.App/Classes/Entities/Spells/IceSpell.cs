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
            string msg = "";
            foreach (Type T in collidables)
            {
                if (CheckCollision(X, Y, Width, Height, T))
                {
                    if (T.IsAssignableFrom(typeof(Character)))
                    {   // If etype is a character
                        // Do damage to enemy character
                        //var enemy = (T)GetCollisionObject(X, Y, Width, Height, T);
                        //enemy.HP -= Damage;

                        msg = "Collision: " + T;
                    }

                    if (T.IsSubclassOf(typeof(Character)))
                    {   // If etype is a character
                        // Do damage to enemy character
                        //var enemy = (T)GetCollisionObject(X, Y, Width, Height, T);
                        //enemy.HP -= Damage;

                        msg = "Collision: " + T;
                    }

                    RemoveEntity(this);
                }
            }

            CanvasDebugger.Debug(this, msg);
        }


        private void CheckWallCollisions()
        {
            // Check both horizontal and vertical collisions to wall
            if (CheckCollision(X + hsp, Y, Width, Height, typeof(Solid))
             || CheckCollision(X, Y + vsp, Width, Height, typeof(Solid)))
            {
                // Remove itself
                RemoveEntity(this);
            }
        }

        private void CheckCharacterCollisions()
        {
            string msg = "";

            // Check both horizontal and vertical collisions to character
            if (CheckCollision(X, Y, Width, Height, typeof(Bunny)))
            {
                msg = "Collision!";

                // Do damage to enemy character
                Bunny enemy = (Bunny)GetCollisionObject(X, Y, Width, Height, typeof(Bunny));
                enemy.HP -= Damage;

                // Remove itself
                RemoveEntity(this);
            }

            CanvasDebugger.Debug(this, msg);
        }
    }
}

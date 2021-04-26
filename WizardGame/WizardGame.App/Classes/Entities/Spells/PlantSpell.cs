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
using WizardGame.App.Classes.Entities.ParticleEffects;
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

        public void Draw(CanvasDrawingSession ds)
        {
            UpdateMovement();
            HandleState();

            if (Sign(hsp) != 0)
            {
                XScale = Sign(hsp);
            }

            rotation += rotationIncrease;

            using (var spriteBatch = ds.CreateSpriteBatch())
            {
                spriteSheet.DrawSpriteExt(
                    spriteBatch,
                    new Vector2(X, Y),
                    new Vector2(ImageX, ImageY),
                    new Vector4(Red, Green, Blue, Alpha),
                    rotation,
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
            hsp = (float)speed * Sign(direction);
            vsp += grv;

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
                if (enemy.Invincibility == false)
                {
                    enemy.HP -= damage;
                    state++;
                }
            }
        }
    }
}

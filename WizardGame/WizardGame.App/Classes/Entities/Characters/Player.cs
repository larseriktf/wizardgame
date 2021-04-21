using Microsoft.Graphics.Canvas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Windows.UI;
using WizardGame.App.Classes.Entities;
using WizardGame.App.Classes.Entities.Dev;
using WizardGame.App.Classes.Entities.Spells;
using WizardGame.App.Interfaces;
using static System.Math;
using static WizardGame.App.Classes.KeyBoard;
using static WizardGame.App.Classes.EntityManager;
using WizardGame.App.Classes.Graphics;

namespace WizardGame.App.Classes.Entities.Characters
{
    public class Player : Entity, IDrawable
    {
        public int MoveSpeed { get; set; } = 10;
        private SpriteSheet sprite = null;
        public readonly int spriteWidth = 96;
        public readonly int spriteHeight = 96;

        private float vsp;
        private float hsp;

        public Player()
        {
            Width = 50;
            Height = 50;
        }

        public void Draw(CanvasDrawingSession ds)
        {
            UpdateMovement();
            RegisterSpells();

            if (Sign(hsp) != 0)
            {
                XScale = Sign(hsp);
            }
            
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
                    ImageLoader.SpriteSheets.TryGetValue("playerSheet", out sprite);
                }
            }
        }


        private void UpdateMovement()
        {
            // Calculate movement
            float moveHorizontal = Convert.ToInt32(KeyRight.Pressed) - Convert.ToInt32(KeyLeft.Pressed);
            float moveVertical = Convert.ToInt32(KeyDown.Pressed) - Convert.ToInt32(KeyUp.Pressed);

            hsp = moveHorizontal * MoveSpeed;
            vsp = moveVertical * MoveSpeed;

            // Horizontal Collision
            if (CheckCollision(X + hsp, Y, Width, Height, typeof(Solid)))
            {
                while (!CheckCollision(X + Sign(hsp), Y, Width, Height, typeof(Solid)))
                {   // Move as close as possible to the entity
                    X += Sign(hsp);
                }
                hsp = 0;
            }

            // Update X
            X += hsp;

            // Horizontal Collision
            if (CheckCollision(X, Y + vsp, Width, Height, typeof(Solid)))
            {
                while (!CheckCollision(X, Y + Sign(vsp), Width, Height, typeof(Solid)))
                {   // Move as close as possible to the entity
                    Y += Sign(vsp);
                }
                vsp = 0;
            }

            // Update Y 
            Y += vsp;
        }

        private void RegisterSpells()
        {
            ArrowUp.EnsureTapped(() =>
            {
                AddEntity("layer2", new IceSpell());
            });
        }
    }
}

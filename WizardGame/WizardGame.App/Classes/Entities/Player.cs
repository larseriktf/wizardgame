using Microsoft.Graphics.Canvas;
using System;
using System.Numerics;
using Windows.UI;
using WizardGame.App.Classes.Entities.Enemies;
using WizardGame.App.Classes.Entities.HudElements;
using WizardGame.App.Classes.Entities.ParticleEffects;
using WizardGame.App.Classes.Entities.Spells;
using WizardGame.App.Classes.Graphics;
using WizardGame.App.Interfaces;
using WizardGame.App.Views;
using static System.Math;
using static WizardGame.App.Classes.EntityManager;
using static WizardGame.App.Classes.Input.KeyBoard;

namespace WizardGame.App.Classes.Entities
{
    public class Player : PhysicsObject, IDrawable
    {
        public readonly int spriteWidth = 96;
        public readonly int spriteHeight = 96;

        public Player()
        {
            moveSpeed = 10;
            spriteSheet = ImageLoader.GetSpriteSheet("sheet_player");
            Width = 50;
            Height = 50;
            AddEntity("layer_hud", new HealthBar());
        }

        public void Update()
        {
            UpdateMovement();
            RegisterSpells();

            if (Sign(hsp) != 0)
            {
                XScale = Sign(hsp);
            }
        }

        public void Draw(CanvasDrawingSession ds)
        {
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

            ds.DrawRectangle(X - Width / 2, Y - Height / 2, Width, Height, Colors.Green);
        }


        private void UpdateMovement()
        {
            // Calculate movement
            float moveHorizontal = Convert.ToInt32(MoveRight.Pressed) - Convert.ToInt32(MoveLeft.Pressed);
            float moveVertical = Convert.ToInt32(MoveDown.Pressed) - Convert.ToInt32(MoveUp.Pressed);

            hsp = moveHorizontal * moveSpeed;
            vsp = moveVertical * moveSpeed;

            UpdateCollisions();

        }

        private void RegisterSpells()
        {
            Action1.EnsureTapped(() =>
            {
                AddEntity("layer2", new Bunny()
                {
                    X = X,
                    Y = Y
                });
            });
            Action2.EnsureTapped(() =>
            {
                AddEntity("layer2", new IceSpell()
                {
                    X = X,
                    Y = Y,
                    Direction = (XScale == 1) ? 0 : PI
                });
            });
            Action3.EnsureTapped(() =>
            {
                AddEntity("layer2", new PlantSpell()
                {
                    X = X,
                    Y = Y,
                    Direction = Sign(XScale)
                });
            });
            Action4.EnsureTapped(() =>
            {
                TeleportationSpell.Teleport();
            });
        }
    }
}

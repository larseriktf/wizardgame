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

        public Player(float x, float y) : base(x, y, 50, 50)
        {
            moveSpeed = 10;
            spriteSheet = ImageLoader.GetSpriteSheet("sheet_player");
            AddEntity("layer_hud", new HealthBar());
            AddEntity("layer_hud", new CrystalOrb());
        }

        public void Update()
        {
            UpdateMovement();
            RegisterSpells();

            if (Sign(hsp) != 0)
            {
                XScale = Sign(hsp);
            }
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
                    0,
                    new Vector2(OffsetXScale, OffsetYScale),
                    0);
            }

            ds.DrawRectangle(OffsetX - OffsetWidth / 2, OffsetY - OffsetHeight / 2, OffsetWidth, OffsetHeight, Colors.Green);
            ds.DrawText("Screen Width: " + Screen.Width, OffsetX, OffsetY, Colors.Blue);
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
                Bunny.Spawner(X, Y);
            });
            Action2.EnsureTapped(() =>
            {
                AddEntity("layer2", new IceSpell(X, Y)
                {
                    Direction = (XScale == 1) ? 0 : PI
                });
            });
            Action3.EnsureTapped(() =>
            {
                AddEntity("layer2", new PlantSpell(X, Y)
                {
                    Direction = Sign(XScale)
                });
            });
            Action4.EnsureTapped(() =>
            {
                TeleportationSpell.Teleport();
            });
        }

        public static void Spawner(float x, float y)
        {
            AddEntity("layer1", new Player(x, y));
        }
    }
}

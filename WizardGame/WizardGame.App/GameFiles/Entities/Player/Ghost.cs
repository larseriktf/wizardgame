using Microsoft.Graphics.Canvas;
using System;
using System.Numerics;
using System.Timers;
using Windows.UI;
using WizardGame.App.GameFiles.Entities.Enemies;
using WizardGame.App.GameFiles.Entities.HudElements;
using WizardGame.App.GameFiles.Entities.ParticleEffects;
using WizardGame.App.GameFiles.Entities.Spells;
using WizardGame.App.GameFiles.Graphics;
using WizardGame.App.Helpers;
using WizardGame.App.Interfaces;
using static System.Math;
using static WizardGame.App.GameFiles.EntityManager;
using static WizardGame.App.GameFiles.Input.KeyBoard;
using static WizardGame.App.Helpers.RandomProvider;

namespace WizardGame.App.GameFiles.Entities.Player
{
    public class Ghost : PhysicsObject, IDrawable
    {
        public bool Invincible { get; set; } = false;
        private readonly int invincibleTime = 250;
        protected readonly Timer invincibilityTimer = new Timer();

        public readonly int spriteWidth = 96;
        public readonly int spriteHeight = 96;
        private static int hp = 100;
        public static int HP
        {
            get => hp;
        }

        public Ghost(float x, float y) : base(x, y, 50, 50)
        {
            moveSpeed = 10;
            spriteSheet = ImageLoader.GetSpriteSheet("sheet_player");

            invincibilityTimer.Elapsed +=
                delegate (object source, ElapsedEventArgs e)
                {
                    Invincible = false;
                };
            invincibilityTimer.Start();
        }

        public void Update()
        {
            UpdateMovement();
            RegisterSpells();

            if (Sign(hsp) != 0)
            {
                XScale = Sign(hsp);
            }

            if (Invincible == true)
            {
                ImageX = 2;
            }
            else
            {
                ImageX = 0;
            }
            OffsetAndScale();

            CanvasDebugger.Debug(this, "HP: " + hp);
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

            //ds.DrawRectangle(OffsetX - OffsetWidth / 2, OffsetY - OffsetHeight / 2, OffsetWidth, OffsetHeight, Colors.Green);
            //ds.DrawText("HP: " + hp, OffsetX, OffsetY, Colors.Blue);
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
            Action1.EnsureTapped(() => AddEntity("layer1", new Bunny(X, Y)));

            Action2.EnsureTapped(() =>
                AddEntity("layer2", new IceSpell(X, Y)
                {
                    Direction = (XScale == 1) ? 0 : PI
                }));

            Action3.EnsureTapped(() =>
                AddEntity("layer2", new PlantSpell(X, Y)
                {
                    Direction = Sign(XScale)
                }));

            Action4.EnsureTapped(() => TeleportationSpell.Teleport());
        }

        public void TakeDamage(int dmg)
        {
            if (Invincible == false)
            {
                if ((hp -= dmg) <= 0)
                {
                    // Die
                    DustCloud.Spawner(X, Y, Rnd.Next(4, 7));
                    RemoveEntity(this);
                }
                invincibilityTimer.Interval = invincibleTime;
                Invincible = true;
            }
        }
    }
}

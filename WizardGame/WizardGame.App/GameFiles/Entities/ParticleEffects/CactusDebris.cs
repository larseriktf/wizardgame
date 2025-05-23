﻿using Microsoft.Graphics.Canvas;
using System.Numerics;
using WizardGame.App.GameFiles.Graphics;
using WizardGame.App.Interfaces;
using static WizardGame.App.GameFiles.EntityManager;
using static WizardGame.App.Helpers.RandomProvider;

namespace WizardGame.App.GameFiles.Entities.ParticleEffects
{
    class CactusDebris : Particle, IDrawable
    {
        private readonly float grv = 0.3f;
        private float hspeed = 0;
        private float vspeed = 0;
        public CactusDebris(float x, float y) : base(x, y, 5, 5)
        {
            spriteSheet = ImageLoader.GetSpriteSheet("sheet_cactus_debris");
            ImageX = Rnd.Next(0, 3);
            hspeed = (float)Rnd.NextDouble() * 4 * (Rnd.Next(0, 2) == 1 ? 1 : -1);
            vspeed = (float)Rnd.NextDouble() * 4 * (Rnd.Next(0, 2) == 1 ? 1 : -1);
        }

        public void Update()
        {
            HandleState();

            vspeed += grv;

            hsp = hspeed;
            vsp = vspeed;

            UpdateCollisions();
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

        public static void Spawner(float x, float y, int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                EntityManager.AddEntity("layer_particles", new CactusDebris(x, y));
            }
        }
    }
}

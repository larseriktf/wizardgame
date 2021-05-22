using Microsoft.Graphics.Canvas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Windows.UI;
using WizardGame.App.Classes.Entities.Enemies;
using WizardGame.App.Interfaces;

namespace WizardGame.App.Classes.Entities.Dev
{
    public class EnemySpawner : Entity, IDrawable
    {
        public int EnemiesToSpawn { get; set; } = 0;
        private int spawnDelay = 1000;
        private Timer spawnTimer = new Timer();
        private bool canSpawn = true;
        private int previousWave = GameStateManager.Wave;
        

        public EnemySpawner(float x, float y) : base(x, y, 32, 32)
        {
            spawnTimer.Elapsed += delegate (object source, ElapsedEventArgs e)
            {
                canSpawn = true;
            };
            spawnTimer.Start();
        }

        public void Update()
        {
            if (GameStateManager.Wave != previousWave)
            {   // New wave started
                EnemiesToSpawn = GameStateManager.EnemyCounter / 2;
                previousWave = GameStateManager.Wave;
            }

            if (EnemiesToSpawn > 0 && canSpawn == true)
            {
                Bunny.Spawner(X, Y);
                EnemiesToSpawn--;
                canSpawn = false;

                spawnTimer.Interval = spawnDelay;
            }
        }

        public void Draw(CanvasDrawingSession ds)
        {
            ds.DrawCircle(
                OffsetX - OffsetWidth / 2,
                OffsetY - OffsetHeight / 2,
                8,
                Colors.Red);
        }

        public static void Spawner(float x, float y)
        {
            EntityManager.AddEntity("layer1", new EnemySpawner(x, y));
        }
    }
}

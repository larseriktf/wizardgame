using Microsoft.Graphics.Canvas;
using System.Timers;
using Windows.UI;
using WizardGame.App.GameFiles.Entities.Enemies;
using WizardGame.App.Interfaces;
using static WizardGame.App.GameFiles.EntityManager;
using static WizardGame.App.Helpers.RandomProvider;

namespace WizardGame.App.GameFiles.Entities.Dev
{
    public class EnemySpawner : Entity, IDrawable
    {
        public int EnemiesToSpawn { get; set; } = 0;

        private int spawnDelay = 1000;
        private Timer spawnTimer = new Timer();
        private bool canSpawn = true;
        private int previousWave = GameManager.Wave;



        public EnemySpawner(float x, float y) : base(x, y, 32, 32)
        {
            spawnTimer.Elapsed += (source, e) =>
            {
                canSpawn = true;
            };
            spawnTimer.Start();
        }

        public void Update()
        {
            if (GameManager.Wave != previousWave)
            {   // New wave started
                EnemiesToSpawn = GameManager.EnemyCounter / 2;
                previousWave = GameManager.Wave;
            }

            if (EnemiesToSpawn > 0 && canSpawn == true)
            {
                SpawnEnemies();
            }
        }

        private void SpawnEnemies()
        {
            int amount = 1;

            if (GameManager.Wave % 10 == 0)
            {
                amount = 5;

                MagicCard.Spawner(
                (7 + Rnd.Next(-2, 3)) * 128 + 64,
                (3 + Rnd.Next(1, 2)) * 128 + 64,
                amount);
            }
            else
            {
                AddEntity("layer1", new Bunny(X, Y));
            }

            EnemiesToSpawn -= amount;
            canSpawn = false;
            spawnTimer.Interval = spawnDelay;
        }

        public void Draw(CanvasDrawingSession ds)
        {
            //ds.DrawCircle(
            //    OffsetX - OffsetWidth / 2,
            //    OffsetY - OffsetHeight / 2,
            //    8,
            //    Colors.Red);
        }

        public static void Spawner(float x, float y)
        {
            AddEntity("layer1", new EnemySpawner(x, y));
        }
    }
}

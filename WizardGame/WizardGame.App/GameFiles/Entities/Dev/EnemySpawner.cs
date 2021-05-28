using Microsoft.Graphics.Canvas;
using System.Timers;
using Windows.UI;
using WizardGame.App.GameFiles.Entities.Enemies;
using WizardGame.App.Interfaces;
using static WizardGame.App.GameFiles.EntityManager;
using static WizardGame.App.Helpers.RandomProvider;

namespace WizardGame.App.GameFiles.Entities.Dev
{
    /// <summary>I used to spawn the desired enemies requested by the GameManager class</summary>
    public class EnemySpawner : Entity, IDrawable
    {
        public int NormalEnemySpawnCount { get; set; } = 0;
        public int SpecialEnemySpawnCount { get; set; } = 0;

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
                NormalEnemySpawnCount = GameManager.NormalEnemies / 2;
                SpecialEnemySpawnCount = GameManager.SpecialEnemies / 2;
                previousWave = GameManager.Wave;
            }

            if (canSpawn == true)
            {
                if (NormalEnemySpawnCount > 0)
                {
                    SpawnNormalEnemies();
                }
                if (SpecialEnemySpawnCount > 0)
                {
                    SpawnSpecialEnemies();
                }

                canSpawn = false;
                spawnTimer.Interval = spawnDelay;
            }
        }

        private void SpawnNormalEnemies()
        {
            AddEntity("layer1", new Bunny(X, Y));

            NormalEnemySpawnCount--;
        }

        private void SpawnSpecialEnemies()
        {
            MagicCard.Spawner(
                (7 + Rnd.Next(-2, 3)) * 128 + 64,
                (3 + Rnd.Next(1, 2)) * 128 + 64,
                SpecialEnemySpawnCount);

            SpecialEnemySpawnCount = 0;
        }

        public void Draw(CanvasDrawingSession ds) { }
    }
}

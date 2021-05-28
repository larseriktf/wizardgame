using Microsoft.Graphics.Canvas;
using System.Timers;
using Windows.UI;
using WizardGame.App.GameFiles.Entities.Enemies;
using WizardGame.App.Interfaces;
using static WizardGame.App.GameFiles.EntityManager;

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
            spawnTimer.Elapsed += delegate (object source, ElapsedEventArgs e)
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
                AddEntity("layer1", new Bunny(X, Y));
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
            AddEntity("layer1", new EnemySpawner(x, y));
        }
    }
}

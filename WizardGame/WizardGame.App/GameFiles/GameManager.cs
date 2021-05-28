using System;
using System.Diagnostics;
using WizardGame.App.GameFiles.Entities.Enemies;
using static WizardGame.App.Helpers.RandomProvider;

namespace WizardGame.App.GameFiles
{
    public static class GameManager
    {
        public static int Wave { get; set; } = 0;
        public static int EnemyCounter { get; set; } = 0;
        public static int EnemiesDefeated { get; set; } = 0;
        public static TimeSpan ElapsedTime { get; set; } = new TimeSpan(0, 0, 0);

        private static int enemyCount = 2;
        private static double damageMultiplier = 1;
        public static Stopwatch GameTimer = new Stopwatch();


        public static void NextWave()
        {
            Wave++;

            if (Wave % 10 == 0)
            {
                SpecialWave();
            }
            else
            {
                NormalWave();
            }
        }

        public static void NormalWave()
        {
            EnemyCounter = enemyCount;

            if (Wave % 5 == 0)
            {
                enemyCount += 2;
                damageMultiplier += 1;
            }
        }

        public static void SpecialWave()
        {
            EnemyCounter = 10;

            MagicCard.Spawner(
                (7 + Rnd.Next(-2, 3)) * 128 + 64,
                (3 + Rnd.Next(1, 2)) * 128 + 64,
                EnemyCounter);
        }
    }
}

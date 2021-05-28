using System;
using System.Diagnostics;
using WizardGame.App.GameFiles.Entities.Enemies;

namespace WizardGame.App.GameFiles
{
    public static class GameManager
    {
        public static int Wave { get; set; } = 0;
        public static int NormalEnemies { get; set; } = 0;
        public static int SpecialEnemies { get; set; } = 0;
        public static int TotalEnemies { get; set; } = 0;
        public static int EnemiesDefeated { get; set; } = 0;
        public static TimeSpan ElapsedTime { get; set; } = new TimeSpan(0, 0, 0);

        private static int enemyCount = 2;
        private static int bunnyDamage = 5;
        public static int BunnyDamage
        {
            get => bunnyDamage;
        }
        public static Stopwatch GameTimer = new Stopwatch();

        public static void NextWave()
        {
            Wave++;

            NormalEnemies = enemyCount;

            if (Wave % 10 == 0)
            {
                SpecialEnemies = 10;
            }
            else if (Wave % 3 == 0)
            {
                enemyCount += 2;
                bunnyDamage += 1;
            }
        }
    }
}

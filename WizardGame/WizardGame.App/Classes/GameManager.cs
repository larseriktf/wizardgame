using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WizardGame.App.Classes.Entities;
using WizardGame.App.Classes.Entities.Enemies;

namespace WizardGame.App.Classes
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
            NormalWave();
        }

        public static void NormalWave()
        {
            EnemyCounter = enemyCount;

            enemyCount += 2;
            damageMultiplier += 0.1;
        }

        public static void SpecialWave()
        {

        }
    }
}

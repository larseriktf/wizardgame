using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WizardGame.App.Classes.Entities;
using WizardGame.App.Classes.Entities.Enemies;

namespace WizardGame.App.Classes
{
    public static class GameStateManager
    {
        public static int Wave { get; set; } = 0;
        public static int EnemyCounter { get; set; } = 0;

        private static int enemyCount = 2;
        private static double damageMultiplier = 1;

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

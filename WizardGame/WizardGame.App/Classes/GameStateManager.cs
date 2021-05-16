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
        public static int Wave { get; set; } = 1;
        public static int EnemyCounter { get; set; } = 0;

        public static void NextWave()
        {
            NormalWave(10, 1);
        }

        public static void NormalWave(int amount, double damageMultiplier)
        {
            if (EntityManager.SingleEntityExists(typeof(Player)))
            {
                Player player = (Player)EntityManager.SingleEntity(typeof(Player));

                for (int i = 0; i < amount; i++)
                {
                    Bunny.Spawner(player.X, player.Y);
                }

                EnemyCounter = amount;
            }
        }

        public static void SpecialWave()
        {

        }
    }
}

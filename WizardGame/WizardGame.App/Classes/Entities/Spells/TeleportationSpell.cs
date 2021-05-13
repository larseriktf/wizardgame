using Microsoft.Graphics.Canvas;
using System.Numerics;
using Windows.UI;
using WizardGame.App.Classes.Entities.ParticleEffects;
using WizardGame.App.Classes.Graphics;
using WizardGame.App.Interfaces;
using static System.Math;
using static WizardGame.App.Classes.RandomProvider;
using static WizardGame.App.Classes.EntityManager;
using WizardGame.App.Classes.Entities.Dev;

namespace WizardGame.App.Classes.Entities.Spells
{
    public class TeleportationSpell : Spell
    {
        public static void Teleport()
        {
            Player player;
            
            if (EntityExists(typeof(Player)))
            {
                player = (Player)SingleEntity(typeof(Player));
            }
            else
            {
                return;
            }

            int distance = 512 + Sign(player.XScale);

            // Spawn particles
            DustCloud.Spawner(player.X, player.Y, Rnd.Next(4, 7));
            Onomatopoeia.Spawner(player.X, player.Y, 0);

            // Actual teleportation
            if (!CheckCollisionMultiple(player.X + distance, player.Y, player.Width, player.Height, typeof(Solid))

             && !(player.X + distance > 1920))
            {
                player.X += distance * Sign(player.XScale);
            }

            // Spawn particles
            DustCloud.Spawner(player.X, player.Y, Rnd.Next(4, 7));
        }
    }
}

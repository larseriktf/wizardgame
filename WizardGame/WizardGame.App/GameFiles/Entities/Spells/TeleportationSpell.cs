using WizardGame.App.GameFiles.Entities.Dev;
using WizardGame.App.GameFiles.Entities.ParticleEffects;
using WizardGame.App.GameFiles.Entities.Player;
using static System.Math;
using static WizardGame.App.GameFiles.EntityManager;
using static WizardGame.App.Helpers.RandomProvider;

namespace WizardGame.App.GameFiles.Entities.Spells
{
    public class TeleportationSpell : Spell
    {
        public TeleportationSpell() : base(0, 0, 0, 0)
        {

        }

        public static void Teleport()
        {
            Ghost player;

            if (EntityExists(typeof(Ghost)))
            {
                player = (Ghost)SingleEntity(typeof(Ghost));
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
            if (!IsColliding(player.X + distance, player.Y, player.Width, player.Height, typeof(Solid))

             && !(player.X + distance > 1920))
            {
                player.X += distance * Sign(player.XScale);
            }

            // Spawn particles
            DustCloud.Spawner(player.X, player.Y, Rnd.Next(4, 7));
        }
    }
}

using WizardGame.App.Classes.Entities.ParticleEffects;
using static WizardGame.App.Classes.EntityManager;

namespace WizardGame.App.Classes.Entities.Characters
{
    public abstract class Character : Collidable
    {
        public int HP { get; set; } = 1;
        public bool Invincibility { get; set; } = false;

        protected virtual void UpdateAliveState<particle>(Entity me) where particle : Particle
        {
            if (HP <= 0)
            {
                RemoveEntity(me);
            }
        }

    }
}

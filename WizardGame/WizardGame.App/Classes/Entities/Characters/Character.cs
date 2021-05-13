using System.Timers;
using WizardGame.App.Classes.Entities.ParticleEffects;
using WizardGame.App.Classes.Entities.Spells;
using static WizardGame.App.Classes.EntityManager;
using static WizardGame.App.Classes.RandomProvider;

namespace WizardGame.App.Classes.Entities.Characters
{
    public abstract class Character : Collidable
    {
        public int HP { get; set; } = 1;
        public bool Invincible { get; set; } = false;
        protected readonly Timer invincibilityTimer = new Timer();

        public Character()
        {
            invincibilityTimer.Elapsed +=
                delegate (object source, ElapsedEventArgs e)
                {
                    Invincible = false;
                };
            invincibilityTimer.Start();
        }

        protected virtual void UpdateAliveState()
        {
            if (HP <= 0)
            {
                DustCloud.Spawner(X, Y, Rnd.Next(4, 7));
                RemoveEntity(this);
            }
        }

        protected virtual void UpdateInvincibility()
        {
            if (CheckCollisionMultiple(X, Y, Width, Height, typeof(Spell))
             && Invincible == false)
            {
                Invincible = true;
                invincibilityTimer.Interval = 1000;
            }
        }

    }
}

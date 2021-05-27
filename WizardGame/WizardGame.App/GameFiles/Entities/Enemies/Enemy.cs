using System.Timers;
using WizardGame.App.GameFiles.Entities.ParticleEffects;
using static WizardGame.App.GameFiles.EntityManager;
using static WizardGame.App.Helpers.RandomProvider;

namespace WizardGame.App.GameFiles.Entities.Enemies
{
    public abstract class Enemy : PhysicsObject
    {
        protected int hp = 1;
        public bool Invincible { get; set; } = false;
        protected readonly Timer invincibilityTimer = new Timer();
        private readonly int invincibleTime = 250;
        protected int state = 0;

        public Enemy(float x, float y, int width, int height) : base(x, y, width, height)
        {
            invincibilityTimer.Elapsed +=
                delegate (object source, ElapsedEventArgs e)
                {
                    Invincible = false;
                };
            invincibilityTimer.Start();
        }

        protected virtual void Die()
        {
            DustCloud.Spawner(X, Y, Rnd.Next(4, 7));
            RemoveEntity(this);
            GameManager.EnemiesDefeated++;
            GameManager.EnemyCounter--;
        }

        public void TakeDamage(int dmg)
        {
            if (Invincible == false)
            {
                if ((hp -= dmg) <= 0)
                {
                    Die();
                }
                invincibilityTimer.Interval = invincibleTime;
                Invincible = true;
            }
        }

    }
}

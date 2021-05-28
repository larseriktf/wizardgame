using System.Timers;
using WizardGame.App.GameFiles.Entities.ParticleEffects;
using WizardGame.App.GameFiles.Entities.Player;
using static WizardGame.App.GameFiles.EntityManager;
using static WizardGame.App.Helpers.RandomProvider;

namespace WizardGame.App.GameFiles.Entities.Enemies
{
    public abstract class Enemy : PhysicsObject
    {
        public bool Invincible { get; set; } = false;
        private readonly int invincibleTime = 250;
        protected readonly Timer invincibilityTimer = new Timer();
        protected int hp = 1;
        protected int damage = 1;
        protected int state = 0;

        public Enemy(float x, float y, int width, int height)
            : base(x, y, width, height)
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

        protected void DamagePlayerOnCollision()
        {
            if (IsColliding(X, Y, Width, Height, typeof(Ghost)))
            {
                // If collided with ghost, Do damage to ghost
                Ghost ghost = (Ghost)GetCollisionObject(X, Y, Width, Height, typeof(Ghost));

                if (ghost.Invincible == false)
                {
                    ghost.TakeDamage(damage);
                    state++;
                }
            }
        }
    }
}

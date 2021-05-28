using System.Timers;

namespace WizardGame.App.GameFiles.Entities.ParticleEffects
{
    public abstract class Particle : PhysicsObject
    {
        protected Timer fadeOutTimer;
        protected int state = 0;

        public Particle(float x, float y,
            int width = 0, int height = 0,
            int lifeSpan = 1000)
            : base(x, y, width, height)
        {
            fadeOutTimer = new Timer(lifeSpan);
            fadeOutTimer.Elapsed += delegate (object source, ElapsedEventArgs e)
            {
                state++;
            };
            fadeOutTimer.Start();
        }
    }
}

using System;
using System.Timers;
using WizardGame.App.Classes.Graphics;

namespace WizardGame.App.Classes.Entities.ParticleEffects
{
    public abstract class Particle : PhysicsObject
    {
        protected Timer fadeOutTimer;
        protected int state = 0;

        public Particle(int lifeSpan = 1000)
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

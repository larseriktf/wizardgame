using System;
using System.Timers;

namespace WizardGame.App.Classes.Entities.ParticleEffects
{
    public abstract class Particle : Collidable
    {
        protected int fadeTime = 1000;
        protected int fadeOutStartTime = new Random().Next(800, 1200);
        protected Timer fadeOutTimer;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using static WizardGame.App.Classes.EntityManager;

namespace WizardGame.App.Classes.Entities.ParticleEffects
{
    public abstract class Particle : Collidable
    {
        protected int fadeTime = 1000;
        protected int fadeOutStartTime = new Random().Next(800, 1200);
        protected Timer fadeOutTimer;
    }
}

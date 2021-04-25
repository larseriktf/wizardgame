using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace WizardGame.App.Classes.Entities.ParticleEffects
{
    public abstract class Particle : Collidable
    {
        protected int fadeTime = 1000;
        protected int fadeOutStartTime = 4000;
        protected Timer fadeOutTimer;
    }
}

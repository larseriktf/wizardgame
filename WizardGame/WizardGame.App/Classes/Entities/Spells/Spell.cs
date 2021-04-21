using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WizardGame.App.Classes.Entities.Spells
{
    public abstract class Spell : Collidable
    {
        public double Angle { get; set; } = 0;
        public double Speed { get; set; } = 0;
        public int Damage { get; set; } = 1;
    }
}

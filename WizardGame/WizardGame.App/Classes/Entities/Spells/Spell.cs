using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WizardGame.App.Classes.Entities.Spells
{
    public abstract class Spell : Entity
    {
        public double Angle { get; set; } = 0;
    }
}

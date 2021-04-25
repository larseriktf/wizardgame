using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WizardGame.App.Classes.Entities.Spells
{
    public abstract class Spell : Collidable
    {
        protected double angle = 0;
        protected double speed = 0;
        protected int damage = 1;
    }
}

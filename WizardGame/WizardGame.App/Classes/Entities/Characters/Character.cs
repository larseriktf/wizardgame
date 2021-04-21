using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WizardGame.App.Classes.Entities.Characters
{
    public abstract class Character : Collidable
    {
        public int HP { get; set; } = 1;
    }
}

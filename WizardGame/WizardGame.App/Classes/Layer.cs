using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WizardGame.App.Classes.Entities;
using WizardGame.App.Interfaces;

namespace WizardGame.App.Classes
{
    public class Layer
    {
        public string Name { get; set; }
        public List<Entity> GameObjects { get; set; } = new List<Entity>();

        public Layer(string name)
        {
            Name = name;
        }
    }
}

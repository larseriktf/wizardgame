using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WizardGame.App.Classes.Entities
{
    public abstract class Entity
    {
        public float XPos { get; set; } = 0;
        public float YPos { get; set; } = 0;
        public SpriteSheet Sprite { get; set; } = null;
        public int ImageX { get; set; } = 0;
        public int ImageY { get; set; } = 0;
        public float XScale { get; set; } = 1f;
        public float YScale { get; set; } = 1f;

        public float Red { get; set; } = 1f;
        public float Green { get; set; } = 1f;
        public float Blue { get; set; } = 1f;
        public float Alpha { get; set; } = 1f;
    }
}

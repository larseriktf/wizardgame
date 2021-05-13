using WizardGame.App.Classes.Graphics;

namespace WizardGame.App.Classes.Entities.Spells
{
    public abstract class Spell : PhysicsObject
    {
        protected int state = 0;
        protected double direction = 0;
        public double Direction
        {
            get
            {
                return direction;
            }
            set
            {
                direction = value;
            }
        }
        protected int damage = 1;
    }
}

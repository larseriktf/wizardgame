using WizardGame.App.Classes.Graphics;

namespace WizardGame.App.Classes.Entities.Spells
{
    public abstract class Spell : PhysicsObject
    {
        protected SpriteSheet spriteSheet;
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

        protected double speed = 12;
        protected int damage = 1;
    }
}

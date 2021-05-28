namespace WizardGame.App.GameFiles.Entities.Spells
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


        public Spell(
            float x, float y,
            int width, int height)
            : base(x, y, width, height)
        {

        }
    }
}

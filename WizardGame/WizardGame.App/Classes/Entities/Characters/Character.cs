namespace WizardGame.App.Classes.Entities.Characters
{
    public abstract class Character : Collidable
    {
        public int HP { get; set; } = 1;
        public bool Invincibility { get; set; } = false;

    }
}

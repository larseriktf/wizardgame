using WizardGame.App.GameFiles.Entities.Dev;
using static System.Math;
using static WizardGame.App.GameFiles.EntityManager;

namespace WizardGame.App.GameFiles.Entities
{
    public abstract class PhysicsObject : Entity
    {
        public static float Gravity { get; set; } = 0.5f;
        protected int moveSpeed = 3;
        protected float vsp;
        protected float hsp;

        protected void UpdateCollisions()
        {
            // Horizontal Collision
            if (IsColliding(X + hsp, Y, Width, Height, typeof(Solid)))
            {
                while (!IsColliding(X + Sign(hsp), Y, Width, Height, typeof(Solid)))
                {   // Move as close as possible to the entity
                    X += Sign(hsp);
                }
                hsp = 0;
            }

            // Update X
            X += hsp;

            // Horizontal Collision
            if (IsColliding(X, Y + vsp, Width, Height, typeof(Solid)))
            {
                while (!IsColliding(X, Y + Sign(vsp), Width, Height, typeof(Solid)))
                {   // Move as close as possible to the entity
                    Y += Sign(vsp);
                }
                vsp = 0;
            }

            // Update Y 
            Y += vsp;
        }

        public PhysicsObject(
            float x, float y,
            int width, int height)
            : base(x, y, width, height)
        {

        }
    }
}

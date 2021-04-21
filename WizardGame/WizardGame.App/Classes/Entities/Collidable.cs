using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WizardGame.App.Classes.Entities.Dev;
using static System.Math;
using static WizardGame.App.Classes.EntityManager;

namespace WizardGame.App.Classes.Entities
{
    public abstract class Collidable : Entity
    {
        protected float vsp;
        protected float hsp;

        protected void UpdateCollisions()
        {
            // Horizontal Collision
            if (CheckCollision(X + hsp, Y, Width, Height, typeof(Solid)))
            {
                while (!CheckCollision(X + Sign(hsp), Y, Width, Height, typeof(Solid)))
                {   // Move as close as possible to the entity
                    X += Sign(hsp);
                }
                hsp = 0;
            }

            // Update X
            X += hsp;

            // Horizontal Collision
            if (CheckCollision(X, Y + vsp, Width, Height, typeof(Solid)))
            {
                while (!CheckCollision(X, Y + Sign(vsp), Width, Height, typeof(Solid)))
                {   // Move as close as possible to the entity
                    Y += Sign(vsp);
                }
                vsp = 0;
            }

            // Update Y 
            Y += vsp;
        }
    }
}

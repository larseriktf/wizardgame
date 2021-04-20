using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using WizardGame.App.Classes.Entities;
using WizardGame.App.Classes.Entities.Dev;
using WizardGame.App.Interfaces;
using static System.Math;

namespace WizardGame.App.Classes
{
    public static class EntityManager
    {

        //public static List<Layer> Layers { get; set; } = new List<Layer>();
        public static Dictionary<string, List<Entity>> Layers { get; set; } = new Dictionary<string, List<Entity>>()
        {
            {"layer0", new List<Entity>() },
            {"layer1", new List<Entity>() },
            {"layer2", new List<Entity>() }
        };

        public static List<Entity> Entities = new List<Entity>();

        public static bool EntityExists(Type className)
        {   // Runs through list of entities and checks if they are of type className
            foreach (Entity entity in Entities)
            {
                if (entity.GetType().Equals(className))
                {
                    return true;
                }
            }
            return false;
        }

        public static bool SingleEntityExists(Type className)
        {
            int occurrences = 0;
            foreach (Entity entity in Entities)
            {
                if (entity.GetType().Equals(className))
                {
                    occurrences++;
                }
            }
            if (occurrences == 1) return true;
            return false;
        }

        public static Entity GetSingleEntity(Type className)
        {
            foreach (Entity entity in Entities)
            {
                if (entity.GetType().Equals(className))
                {
                    return entity;
                }
            }
            return null;
        }

        public static Entity GetNearestEntity(Entity obj, Type className)
        {
            List<Entity> entities = GetEntities(className);
            Entity nearest = null;
            double dist = 0;

            foreach (Entity entity in entities)
            {
                if (nearest == null)
                {
                    nearest = entity;
                    dist = GetDistanceBetweenEntities(obj, entity);
                }
                else if (GetDistanceBetweenEntities(obj, entity) < dist)
                {
                    nearest = entity;
                }
            }
            return nearest;
        }

        public static List<Entity> GetEntities(Type className)
        {
            List<Entity> listOfObjects = new List<Entity>();
            foreach (Entity entity in Entities)
            {
                if (entity.GetType().Equals(className))
                {
                    listOfObjects.Add(entity);
                }
            }
            return listOfObjects;
        }

        public static List<Solid> GetAllSolidEntities()
        {
            List<Solid> listOfSolids = new List<Solid>();

            foreach (Entity entity in Entities)
            {
                if (entity.GetType().IsSubclassOf(typeof(Solid)))
                {
                    listOfSolids.Add((Solid)entity);
                }
            }

            return listOfSolids;
        }

        public static double GetAngleBetweenEntitiesInRadians(Entity objA, Entity objB)
        {
            // Vector between objA and objB
            Vector2 a = new Vector2(objB.X - objA.X, objB.Y - objA.Y);

            // Horizontal right vector
            Vector2 b = new Vector2(1, 0);

            // Calculate angle (theta) in radians
            // Thanks to https://www.youtube.com/watch?v=_VuZZ9_58Wg
            float crossProduct = GetCrossProductOfTwoVectors(a, b);
            double angle = Atan2(Abs(crossProduct), Vector2.Dot(a, b));

            if (crossProduct > 0)
            {
                angle = (2 * PI) - angle;
            }

            return angle;
        }

        public static float GetCrossProductOfTwoVectors(Vector2 a, Vector2 b)
        {
            return a.X * b.Y - a.Y * b.X; ;
        }

        public static double GetDistanceBetweenEntities(Entity objA, Entity objB)
        {
            double x = objB.X - objA.X;
            double y = objB.Y - objA.Y;

            double distance = Sqrt(Pow(x, 2) + Pow(y, 2));
            return distance;
        }



        public static bool CheckCollision(float x, float y, int width, int height, Type className)
        {
            List<Entity> entities = GetEntities(className);

            foreach (Entity entity in entities)
            {   // Run four checks to see if it collides
                if ((x + width / 2) >= entity.X
                 && (x - width / 2) <= (entity.X + entity.Width)
                 && (y + height / 2) >= entity.Y
                 && (y - height / 2) <= (entity.Y + entity.Height))
                {   // Collision detected!
                    return true;
                }
            }
            return false;
        }

        //public static bool CheckCollisionBBox(Entity obj, Type className)
        //{
        //    List<Entity> entities = GetEntities(className);

        //    foreach (Entity entity in entities)
        //    {   // Run four checks to see if it collides
        //        if ((obj.XPos + obj.Width - (obj.Width / 2)) >= entity.XPos
        //         && obj.XPos - (obj.Width / 2) <= (entity.XPos + entity.Width)
        //         && (obj.YPos + obj.Width - (obj.Height / 2)) >= entity.YPos
        //         && obj.YPos - (obj.Height / 2) <= (entity.YPos + entity.Height))
        //        {   // Collision detected!
        //            return true;
        //        }
        //    }
        //    return false;
        //}
    }
}

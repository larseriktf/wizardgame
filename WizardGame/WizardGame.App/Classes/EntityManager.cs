using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
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
        public static List<string> Layers { get; } = new List<string>()
        {
            "layer0",
            "layer1",
            "layer2"
        };

        private static readonly List<Entity> entities = new List<Entity>();
        public static List<Entity> Entities
        {
            get
            {
                return entities;
            }
        }

        public static void AddEntity(string layer, Entity entity)
        {
            // Asign layer to entity
            entity.Layer = layer;

            // Insert entity at the correct index (group by layer)
            int myLevel = Layers.IndexOf(layer);
            int index = 0;

            for (int i = 0; i < entities.Count; i++)
            {
                int theirLevel = Layers.IndexOf(entities[i].Layer);

                index = i;

                if (myLevel < theirLevel)
                {
                    break;
                }
            }

            entities.Insert(index, entity);
        }

        public static Entity RemoveEntity(Entity obj)
        {
            if (entities.Contains(obj))
            {
                entities.Remove(obj);
                return obj;
            }
            return null;
        }

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

        public static List<Entity> GetParentAndChildEntities(Type className)
        {
            List<Entity> listOfObjects = new List<Entity>();
            foreach (Entity entity in Entities)
            {
                if (entity.GetType().Equals(className)
                 || className.IsAssignableFrom(entity.GetType()))
                {
                    listOfObjects.Add(entity);
                }
            }
            return listOfObjects;
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
            List<Entity> entities = GetParentAndChildEntities(className);

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

        public static Entity GetCollisionObject(float x, float y, int width, int height, Type className)
        {

            // Get list of entities at are either of className, or a child member of className
            List<Entity> entities = GetParentAndChildEntities(className);

            foreach (Entity entity in entities)
            {   // Run four checks to see if it collides
                if ((x + width / 2) >= entity.X
                 && (x - width / 2) <= (entity.X + entity.Width)
                 && (y + height / 2) >= entity.Y
                 && (y - height / 2) <= (entity.Y + entity.Height))
                {   // Collision detected! Return object
                    return entity;
                }
            }

            return null;
        }


    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using WizardGame.App.GameFiles.Entities;
using static System.Math;

namespace WizardGame.App.GameFiles
{
    public static class EntityManager
    {
        public static List<string> Layers { get; } = new List<string>()
        {
            "layer0",
            "layer1",
            "layer2",
            "layer_particles",
            "layer_hud"
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
            foreach (Entity entity in entities.ToList())
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
            foreach (Entity entity in entities.ToList())
            {
                if (entity.GetType().Equals(className))
                {
                    occurrences++;
                }
            }
            if (occurrences == 1) return true;
            return false;
        }

        public static Entity SingleEntity(Type className)
        {
            foreach (Entity entity in entities.ToList())
            {
                if (entity.GetType().Equals(className))
                {
                    return entity;
                }
            }
            return null;
        }

        public static Entity NearestEntity(Entity obj, Type className)
        {
            List<Entity> entities = GetEntities(className);
            Entity nearest = null;
            double dist = 0;

            foreach (Entity entity in entities)
            {
                if (nearest == null)
                {
                    nearest = entity;
                    dist = DistanceBetweenEntities(obj, entity);
                }
                else if (DistanceBetweenEntities(obj, entity) < dist)
                {
                    nearest = entity;
                }
            }
            return nearest;
        }

        public static List<Entity> GetEntities(Type className)
        {
            List<Entity> listOfObjects = new List<Entity>();
            foreach (Entity entity in entities.ToList())
            {
                if (entity != null)
                {   // Has to check if entity is not null, or else it might throw exception
                    if (entity.GetType().Equals(className)
                     || className.IsAssignableFrom(entity.GetType()))
                    {
                        listOfObjects.Add(entity);
                    }
                }
            }
            return listOfObjects;
        }

        public static double AngleBetweenEntitiesInRadians(Entity objA, Entity objB)
        {
            // Vector between objA and objB
            Vector2 a = new Vector2(objB.X - objA.X, objB.Y - objA.Y);

            // Horizontal right vector
            Vector2 b = new Vector2(1, 0);

            // Calculate angle (theta) in radians
            // Thanks to https://www.youtube.com/watch?v=_VuZZ9_58Wg
            float crossProduct = CrossProductOfTwoVectors(a, b);
            double angle = Atan2(Abs(crossProduct), Vector2.Dot(a, b));

            if (crossProduct > 0)
            {
                angle = (2 * PI) - angle;
            }

            return angle;
        }

        public static float CrossProductOfTwoVectors(Vector2 a, Vector2 b)
        {
            return a.X * b.Y - a.Y * b.X; ;
        }

        public static double DistanceBetweenEntities(Entity objA, Entity objB)
        {
            double x = objB.X - objA.X;
            double y = objB.Y - objA.Y;

            double distance = Sqrt(Pow(x, 2) + Pow(y, 2));
            return distance;
        }

        public static bool IsColliding(float x, float y, int width, int height, Type className)
        {
            List<Entity> entities = GetEntities(className);

            foreach (Entity entity in entities)
            {
                if (CheckCollisionSingle(x, y, width, height, entity))
                {
                    return true;
                }
            }
            return false;
        }

        public static bool CheckCollisionSingle(float x, float y, int width, int height, Entity entity)
        {
            // Run four checks to see if it collides
            if ((x + width / 2) > (entity.X - entity.Width / 2)
             && (x - width / 2) < (entity.X + entity.Width / 2)
             && (y + height / 2) > (entity.Y - entity.Height / 2)
             && (y - height / 2) < (entity.Y + entity.Height / 2))
            {   // Collision detected!
                return true;
            }
            return false;
        }
        public static Entity GetCollisionObject(float x, float y, int width, int height, Type className)
        {
            // Get list of entities at are either of className, or a child member of className
            List<Entity> entities = GetEntities(className);

            foreach (Entity entity in entities)
            {
                if (CheckCollisionSingle(x, y, width, height, entity))
                {
                    return entity;
                }
            }
            return null;
        }

        public static bool IsOverlapping(float x, float y, int width, int height, Type className)
        {
            List<Entity> entities = GetEntities(className);

            foreach (Entity entity in entities)
            {
                if (CheckCollisionSingle(x, y, width, height, entity))
                {
                    return true;
                }
            }
            return false;
        }

        public static bool IsOverlappingSingle(float x, float y, int width, int height, Entity entity)
        {
            // Run four checks to see if it collides
            if ((x + width / 2) > (entity.X - entity.Width / 2)
             && (x - width / 2) < (entity.X + entity.Width / 2)
             && (y + height / 2) > (entity.Y - entity.Height / 2)
             && (y - height / 2) < (entity.Y + entity.Height / 2))
            {   // Collision detected!
                return true;
            }
            return false;
        }

    }
}

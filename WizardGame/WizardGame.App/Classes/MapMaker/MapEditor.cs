using Microsoft.Graphics.Canvas;
using System.Collections.Generic;
using WizardGame.App.Classes.Graphics;
using WizardGame.App.Classes.MapMaker;

namespace WizardGame.App.Classes
{
    public static class MapEditor
    {
        public static List<Map> Maps { get; set; } = new List<Map>();

        public static void MakeMaps()
        {
            // Map 1

            int[][] collisions =
            {
                new int[] {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                new int[] {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
                new int[] {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
                new int[] {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
                new int[] {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
                new int[] {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
                new int[] {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
                new int[] {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                new int[] {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1}
            };

            Maps.Add(new Map()
            {
                CollisionLayout = new CollisionLayout(collisions),
                LevelBackground = new LevelBackground(0, 0)
            });
        }

        public static void LoadMap(int mapIndex, CanvasDevice device)
        {
            Map map = Maps[mapIndex];

            map.CollisionLayout.GenerateLayout();

            foreach (MapLayout layout in map.MapLayouts)
            {
                //EntityManager.Layers["layer0"].Add(layout);
                EntityManager.AddEntity("layer0", layout);
            }

            //EntityManager.Layers["layer0"].Add(map.LevelBackground);
            EntityManager.AddEntity("layer0", map.LevelBackground);
        }
    }
}

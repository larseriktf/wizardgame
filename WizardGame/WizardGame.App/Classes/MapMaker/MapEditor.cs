using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.UI.Xaml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WizardGame.App.Classes.MapMaker;

namespace WizardGame.App.Classes
{
    public static class MapEditor
    {
        public static List<Map> Maps { get; set; } = new List<Map>();

        public static void MakeMaps()
        {
            // Map 1
            //int[][] testMapArr =
            //{
            //    new int[] {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
            //    new int[] {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
            //    new int[] {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
            //    new int[] {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
            //    new int[] {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
            //    new int[] {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
            //    new int[] {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
            //    new int[] {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
            //    new int[] {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1}

            //};

            int[][] testMapCollisions =
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
                //MapLayouts = new MapLayout[]
                //{
                //    new MapLayout("ms-appx:///Assets/Sprites/Dev/spr_dev.jpg", testMapArr)
                //},
                CollisionLayout = new CollisionLayout(testMapCollisions),
                LevelBackground = new LevelBackground("ms-appx:///Assets/Sprites/Levels/abandonedRoom.png")
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

﻿using Microsoft.Graphics.Canvas;
using System.Collections.Generic;
using WizardGame.App.GameFiles.MapMaker;

namespace WizardGame.App.GameFiles
{
    public static class MapEditor
    {
        public static List<Map> Maps { get; set; } = new List<Map>();

        public static void MakeMaps()
        {
            // Map 1

            int[][] collisions =
            {
                new int[] {0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0},
                new int[] {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
                new int[] {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
                new int[] {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
                new int[] {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
                new int[] {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
                new int[] {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
                new int[] {0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0},
                new int[] {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}
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
                EntityManager.AddEntity("layer0", layout);
            }

            EntityManager.AddEntity("layer0", map.LevelBackground);
        }
    }
}

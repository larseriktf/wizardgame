﻿using WizardGame.App.GameFiles.Entities;
using WizardGame.App.GameFiles.Entities.Dev;
using WizardGame.App.Interfaces;

namespace WizardGame.App.GameFiles.MapMaker
{
    public class CollisionLayout : Entity, ILayout
    {
        public int[][] Layout { get; set; }

        public CollisionLayout(int[][] layout) : base(0, 0)
        {
            Layout = layout;
        }

        public void GenerateLayout()
        {
            for (int y = 0; y < Layout.Length; y++)
            {
                for (int x = 0; x < Layout[y].Length; x++)
                {
                    if (Layout[y][x] == 1)
                    {
                        EntityManager.Entities.Add(
                            new Solid(x * 128 + 64, y * 128 + 64));
                    }
                }
            }
        }
    }
}

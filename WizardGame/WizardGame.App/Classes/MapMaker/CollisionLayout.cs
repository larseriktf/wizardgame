using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WizardGame.App.Classes.Entities;
using WizardGame.App.Classes.Entities.Dev;

namespace WizardGame.App.Classes.MapMaker
{
    public class CollisionLayout : Entity
    {
        public int[][] Layout { get; set; }

        public CollisionLayout(int[][] layout)
        {
            Layout = layout;
        }

        public void Generate()
        {
            for (int y = 0; y < Layout.Length; y++)
            {
                for (int x = 0; x < Layout[y].Length; x++)
                {
                    if (Layout[y][x] == 1)
                    {
                        EntityManager.gameEntities.Add(
                            new Solid()
                            {
                                X = x * 128,
                                Y = y * 128
                            });
                    }
                }
            }
        }
    }
}

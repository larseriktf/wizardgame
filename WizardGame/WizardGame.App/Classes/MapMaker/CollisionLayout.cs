using WizardGame.App.Classes.Entities;
using WizardGame.App.Classes.Entities.Dev;
using WizardGame.App.Interfaces;

namespace WizardGame.App.Classes.MapMaker
{
    public class CollisionLayout : Entity, ILayout
    {
        public int[][] Layout { get; set; }

        public CollisionLayout(int[][] layout)
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

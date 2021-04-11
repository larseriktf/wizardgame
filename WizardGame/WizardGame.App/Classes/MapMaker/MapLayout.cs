using Microsoft.Graphics.Canvas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using WizardGame.App.Classes.Entities;
using WizardGame.App.Interfaces;

namespace WizardGame.App.Classes.MapMaker
{
    public class MapLayout : Entity
    {
        public int[][] Layout { get; set; } // multidimensional array

        public string BitMapUri => throw new NotImplementedException();

        public MapLayout(SpriteSheet spriteSheet, int[][] layout)
        {
            Sprite = spriteSheet;
            Layout = layout;
        }

        public void DrawSelf(CanvasDrawingSession ds)
        {
            using (var spriteBatch = ds.CreateSpriteBatch())
            {
                for (int x = 0; x < Layout.Length; x++)
                {
                    for (int y = 0; y < Layout[x].Length; y++)
                    {
                        if (Layout[y][x] == 1)
                        {
                            Sprite.DrawSpriteExt(
                            spriteBatch,
                            new Vector2(x * 128, y * 128),
                            new Vector2(0, 0),
                            new Vector4(1, 1, 1, 1),
                            0,
                            new Vector2(1, 1),
                            0);
                        }
                    }
                }
            }
        }
    }
}

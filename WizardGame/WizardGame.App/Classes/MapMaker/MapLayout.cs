using Microsoft.Graphics.Canvas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using WizardGame.App.Classes.Entities;
using WizardGame.App.Classes.Graphics;
using WizardGame.App.Interfaces;

namespace WizardGame.App.Classes.MapMaker
{
    public class MapLayout : Entity, IDrawable, ILayout
    {
        public int[][] Layout { get; set; } // multidimensional array
        private SpriteSheet sprite = null;
        public string BitMapUri { get; set; }

        public MapLayout(string bitMapUri, int[][] layout)
        {
            BitMapUri = bitMapUri;
            Layout = layout;
        }

        public void Draw(CanvasDrawingSession ds)
        {
            if (sprite != null)
            {
                using (var spriteBatch = ds.CreateSpriteBatch())
                {
                    for (int y = 0; y < Layout.Length; y++)
                    {
                        for (int x = 0; x < Layout[y].Length; x++)
                        {
                            if (Layout[y][x] == 1)
                            {
                                sprite.DrawSpriteExt(
                                spriteBatch,
                                new Vector2(x * 128 + 64, y * 128 + 64),
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
            else
            {

            }
        }
    }
}

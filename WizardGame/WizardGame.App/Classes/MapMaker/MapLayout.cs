using Microsoft.Graphics.Canvas;
using System.Numerics;
using WizardGame.App.Classes.Entities;
using WizardGame.App.Classes.Graphics;
using WizardGame.App.Interfaces;

namespace WizardGame.App.Classes.MapMaker
{
    // @TODO: Remove this class, as it is unecessary right now
    public class MapLayout : Entity, IDrawable, ILayout
    {
        public int[][] Layout { get; set; } // multidimensional array
        private SpriteSheet sprite = null;
        public string BitMapUri { get; set; }

        public MapLayout(string bitMapUri, int[][] layout) : base(0, 0)
        {
            BitMapUri = bitMapUri;
            Layout = layout;
        }

        public void Update()
        {

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

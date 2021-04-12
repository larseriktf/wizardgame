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
    public class MapLayout : Entity, IDrawable
    {
        public int[][] Layout { get; set; } // multidimensional array
        public SpriteSheet Sprite { get; set; } = null;
        public string BitMapUri { get; set; }

        public MapLayout(string bitMapUri, int[][] layout)
        {
            BitMapUri = bitMapUri;
            Layout = layout;
        }

        public async void LoadImageResourceAsync(CanvasDevice device)
        {
            Sprite = await SpriteSheet.LoadSpriteSheetAsync(device, BitMapUri, new Vector2(128, 128));
        }

        public void DrawSelf(CanvasDrawingSession ds)
        {
            if (Sprite != null)
            {
                using (var spriteBatch = ds.CreateSpriteBatch())
                {
                    for (int y = 0; y < Layout.Length; y++)
                    {
                        for (int x = 0; x < Layout[y].Length; x++)
                        {
                            if (Layout[y][x] == 1)
                            {
                                Sprite.DrawSpriteExt(
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
        }
    }
}

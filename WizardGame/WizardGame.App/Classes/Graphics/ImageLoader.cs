using Microsoft.Graphics.Canvas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace WizardGame.App.Classes.Graphics
{
    public static class ImageLoader
    {
        public static Dictionary<string, SpriteSheet> SpriteSheets { get; set; } = new Dictionary<string, SpriteSheet>();
        public static Dictionary<string, CanvasBitmap> BitMaps { get; set; } = new Dictionary<string, CanvasBitmap>();

        public static async void LoadImageResourceAsync(CanvasDevice device)
        {
            SpriteSheets.Add("playerSheet", await SpriteSheet.LoadSpriteSheetAsync(
                device,
                "ms-appx:///Assets/Sprites/Characters/Player/spr_player.png",
                new Vector2(96, 96)));
        }
    }
}

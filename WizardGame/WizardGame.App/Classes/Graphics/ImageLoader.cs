using Microsoft.Graphics.Canvas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using static WizardGame.App.Classes.Graphics.SpriteSheet;

namespace WizardGame.App.Classes.Graphics
{
    public static class ImageLoader
    {
        public static Dictionary<string, SpriteSheet> SpriteSheets { get; set; } = new Dictionary<string, SpriteSheet>();
        public static Dictionary<string, CanvasBitmap> Bitmaps { get; set; } = new Dictionary<string, CanvasBitmap>();

        public static async Task LoadImageResourceAsync(CanvasDevice device)
        {
            // Load SpriteSheets
            SpriteSheets.Add("playerSheet", await LoadSpriteSheetAsync(
                device,
                "ms-appx:///Assets/Sprites/Characters/Player/spr_player.png",
                new Vector2(96, 96)));

            SpriteSheets.Add("cardSheet", await LoadSpriteSheetAsync(
                device,
                "ms-appx:///Assets/Sprites/Characters/CardEnemy/spr_cards.png",
                new Vector2(24, 24)));

            SpriteSheets.Add("bunnySheet", await LoadSpriteSheetAsync(
                device,
                "ms-appx:///Assets/Sprites/Characters/Bunny/spr_bunny.png",
                new Vector2(96, 96)));

            SpriteSheets.Add("iceSpellSheet", await LoadSpriteSheetAsync(
                device,
                "ms-appx:///Assets/Sprites/Spells/spr_spell_ice.png",
                new Vector2(96, 48)));

            // Load BitMaps
            Bitmaps.Add("level1", await CanvasBitmap.LoadAsync(
                device,
                new Uri("ms-appx:///Assets/Sprites/Levels/abandonedRoom.png")));

            Bitmaps.Add("targetBitmap", await CanvasBitmap.LoadAsync(
                device,
                new Uri("ms-appx:///Assets/Sprites/Levels/abandonedRoom.png")));
        }

        public static SpriteSheet GetSpriteSheet(string key)
        {
            SpriteSheets.TryGetValue(key, out SpriteSheet spriteSheet);
            return spriteSheet;
        }

        public static CanvasBitmap GetBitMap(string key)
        {
            Bitmaps.TryGetValue(key, out CanvasBitmap bitmap);
            return bitmap;
        }
    }
}

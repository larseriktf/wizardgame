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
        private readonly static string defaultPath = "ms-appx:///Assets/Sprites";

        public static async Task LoadImageResourceAsync(CanvasDevice device)
        {
            // Load SpriteSheets
            SpriteSheets.Add(
                "sheet_player",
                await LoadSpriteSheetAsync(
                    device,
                    defaultPath + "/Characters/Player/spr_player.png",
                    new Vector2(96, 96)));

            SpriteSheets.Add(
                "sheet_cardenemy",
                await LoadSpriteSheetAsync(
                    device,
                    defaultPath + "/Characters/CardEnemy/spr_cards.png",
                    new Vector2(24, 24)));

            SpriteSheets.Add(
                "sheet_bunny",
                await LoadSpriteSheetAsync(
                    device,
                    defaultPath + "/Characters/Bunny/spr_bunny.png",
                    new Vector2(96, 96)));

            SpriteSheets.Add(
                "sheet_ice_spell",
                await LoadSpriteSheetAsync(
                    device,
                    defaultPath + "/Spells/spr_spell_ice.png",
                    new Vector2(96, 48)));

            SpriteSheets.Add(
                "sheet_ice_particle",
                await LoadSpriteSheetAsync(
                    device,
                    defaultPath + "/ParticleEffects/spr_ice_shards.png",
                    new Vector2(24, 24)));

            SpriteSheets.Add(
                "sheet_dust_particle",
                await LoadSpriteSheetAsync(
                    device,
                    defaultPath + "/ParticleEffects/spr_dust_cloud.png",
                    new Vector2(128, 128)));

            // Load BitMaps
            Bitmaps.Add(
                "bitmap_level1",
                await CanvasBitmap.LoadAsync(
                    device,
                    new Uri(defaultPath + "/Levels/abandonedRoom.png")));

            Bitmaps.Add(
                "bitmap_target",
                await CanvasBitmap.LoadAsync(
                    device,
                    new Uri(defaultPath + "/Levels/abandonedRoom.png")));
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

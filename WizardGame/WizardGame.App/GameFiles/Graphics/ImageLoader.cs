﻿using Microsoft.Graphics.Canvas;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Threading.Tasks;
using static WizardGame.App.GameFiles.Graphics.SpriteSheet;

namespace WizardGame.App.GameFiles.Graphics
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
                    defaultPath + "/Characters/spr_player.png",
                    new Vector2(96, 96)));

            SpriteSheets.Add(
                "sheet_cardenemy",
                await LoadSpriteSheetAsync(
                    device,
                    defaultPath + "/Characters/spr_cards.png",
                    new Vector2(24, 24)));

            SpriteSheets.Add(
                "sheet_bunny",
                await LoadSpriteSheetAsync(
                    device,
                    defaultPath + "/Characters/spr_bunny.png",
                    new Vector2(96, 96)));

            SpriteSheets.Add(
                "sheet_ice_spell",
                await LoadSpriteSheetAsync(
                    device,
                    defaultPath + "/Spells/spr_spell_ice.png",
                    new Vector2(96, 48)));

            SpriteSheets.Add(
                "sheet_plant_spell",
                await LoadSpriteSheetAsync(
                    device,
                    defaultPath + "/Spells/spr_spell_plant.png",
                    new Vector2(64, 64)));

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

            SpriteSheets.Add(
                "sheet_onomatopoeia_particle",
                await LoadSpriteSheetAsync(
                    device,
                    defaultPath + "/ParticleEffects/spr_onomatopoeia.png",
                    new Vector2(128, 128)));

            SpriteSheets.Add(
                "sheet_health_bar",
                await LoadSpriteSheetAsync(
                    device,
                    defaultPath + "/Hud/spr_health_bar.png",
                    new Vector2(512, 96)));

            SpriteSheets.Add(
                "sheet_cactus_enemy",
                await LoadSpriteSheetAsync(
                    device,
                    defaultPath + "/Characters/spr_cactus_enemy.png",
                    new Vector2(64, 64)));

            SpriteSheets.Add(
                "sheet_cactus_debris",
                await LoadSpriteSheetAsync(
                    device,
                    defaultPath + "/ParticleEffects/spr_cactus_debris.png",
                    new Vector2(24, 24)));

            SpriteSheets.Add(
                "sheet_ice_crystals",
                await LoadSpriteSheetAsync(
                    device,
                    defaultPath + "/ParticleEffects/spr_ice_crystals.png",
                    new Vector2(24, 24)));

            SpriteSheets.Add(
                "sheet_levels",
                await LoadSpriteSheetAsync(
                    device,
                    defaultPath + "/Levels/spr_levels.png",
                    new Vector2(1920, 1152)));

            SpriteSheets.Add(
                "sheet_crystal_orb",
                await LoadSpriteSheetAsync(
                    device,
                    defaultPath + "/Hud/spr_crystal_ball.png",
                    new Vector2(96, 96)));

            // Load BitMaps

            Bitmaps.Add(
                "bitmap_target",
                await CanvasBitmap.LoadAsync(
                    device,
                    new Uri(defaultPath + "/Dev/spr_point.jpg")));
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

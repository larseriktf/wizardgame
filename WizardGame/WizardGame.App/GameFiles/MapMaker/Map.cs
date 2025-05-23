﻿using WizardGame.App.GameFiles.MapMaker;

namespace WizardGame.App.GameFiles
{
    public class Map
    {
        public CollisionLayout CollisionLayout { get; set; }
        public MapLayout[] MapLayouts { get; set; } = new MapLayout[0];
        public LevelBackground LevelBackground { get; set; }
    }
}

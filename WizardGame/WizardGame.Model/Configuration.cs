using System;

namespace WizardGame.Model
{
    public class Configuration
    {
        // General Settings
        private int volume = 50;
        public int Volume
        {
            get
            {
                return volume;
            }
            set
            {   // Making sure volume does not go above 100 or below 0
                if (value > 100)
                {
                    value = 100;
                }
                else if (value < 0)
                {
                    value = 0;
                }
                volume = value;
            }
        }

        
        private int displayMode = 0;
        public int DisplayMode
        {   // 0 = Winowed, 1 = Windowed Borderless, 2 = Fullscreen
            get
            {
                return displayMode;
            }
            set
            {   // Making sure volume does not go above 2 or below 0
                if (value > 2)
                {
                    value = 2;
                }
                else if (value < 0)
                {
                    value = 0;
                }
                displayMode = value;
            }
        }

        // Keybindings
        public char NavContinue { get; set; } = 'W';
        public char NavPause { get; set; } = 'W';
        public char NavBack { get; set; } = 'W';

        public char MoveUp { get; set; } = 'W';
        public char MoveLeft { get; set; } = 'A';
        public char MoveDown { get; set; } = 'S';
        public char MoveRight { get; set; } = 'D';

        public char Action1 { get; set; } = 'U';
        public char Action2 { get; set; } = 'R';
        public char Action3 { get; set; } = 'D';
        public char Action4 { get; set; } = 'L';

        public char Interact1 { get; set; } = 'L';
        public char Interact2 { get; set; } = 'L';
        public char Interact3 { get; set; } = 'L';

    }
}

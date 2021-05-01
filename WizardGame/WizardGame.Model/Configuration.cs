using System.ComponentModel.DataAnnotations;

namespace WizardGame.Model
{
    public class Configuration
    {
        [Key]
        public int Id { get; set; }

        // General Settings
        public int Volume { get; set; } = 50;
        [Required]
        public int DisplayMode { get; set; } = 0; // 0 = Winowed, 1 = Windowed Borderless, 2 = Fullscreen

        // Keybindings
        public string NavContinue { get; set; } = "ENTER";
        public string NavPause { get; set; } = "ESCAPE";
        public string NavBack { get; set; } = "BACKSPACE";

        public string MoveUp { get; set; } = "W";
        public string MoveLeft { get; set; } = "A";
        public string MoveDown { get; set; } = "S";
        public string MoveRight { get; set; } = "D";

        public string Action1 { get; set; } = "UPARROW";
        public string Action2 { get; set; } = "RIGHTARROW";
        public string Action3 { get; set; } = "DOWNARROW";
        public string Action4 { get; set; } = "LEFTARROW";

        public char Interact1 { get; set; } = 'R';
        public char Interact2 { get; set; } = 'F';
        public char Interact3 { get; set; } = 'C';

    }
}

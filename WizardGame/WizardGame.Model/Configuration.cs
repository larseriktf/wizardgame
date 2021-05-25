using System.ComponentModel.DataAnnotations;

namespace WizardGame.Model
{
    public class Configuration
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string ConfigurationName { get; set; }

        // Keybindings
        public string NavContinue { get; set; } = "ENTER";
        public string NavPause { get; set; } = "ESCAPE";
        public string NavBack { get; set; } = "BACKSPACE";

        public string MoveLeft { get; set; } = "A";
        public string MoveUp { get; set; } = "W";
        public string MoveRight { get; set; } = "D";
        public string MoveDown { get; set; } = "S";

        public string Action1 { get; set; } = "LEFTARROW";
        public string Action2 { get; set; } = "UPARROW";
        public string Action3 { get; set; } = "RIGHTARROW";
        public string Action4 { get; set; } = "DOWNARROW";

        public string Interact1 { get; set; } = "R";
        public string Interact2 { get; set; } = "F";
        public string Interact3 { get; set; } = "C";

    }
}

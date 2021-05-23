using System.ComponentModel.DataAnnotations;

namespace WizardGame.Model
{
    public class GameStatistic
    {
        [Key]
        public int Id { get; set; }
        public int WavesPlayed { get; set; } = 0;
        public int EnemiesDefeated { get; set; } = 0;
        public int MinutesElapsed { get; set; } = 0;
    }
}

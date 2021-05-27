using System;
using System.ComponentModel.DataAnnotations;

namespace WizardGame.Model
{
    public class GameStatistic
    {
        [Key]
        public int Id { get; set; }
        public int WavesPlayed { get; set; } = 0;
        public int EnemiesDefeated { get; set; } = 0;
        public TimeSpan ElapsedTime { get; set; } = new TimeSpan(0, 0, 0);

        // Navigation Properties
        public int PlayerProfileId { get; set; }
        public PlayerProfile PlayerProfile { get; set; }
    }
}

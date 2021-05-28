using System;
using System.ComponentModel.DataAnnotations;

namespace WizardGame.Model
{
    public class GameData
    {
        [Key]
        public int Id { get; set; }
        public int WavesPlayed { get; set; } = 0;
        public int EnemiesDefeated { get; set; } = 0;
        public TimeSpan ElapsedTime { get; set; } = new TimeSpan(0, 0, 0);
        public int PlayerId { get; set; }

        // Navigation Properties
        public Player Player { get; set; }
    }
}

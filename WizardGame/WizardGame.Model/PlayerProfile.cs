using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WizardGame.Model
{
    public class PlayerProfile
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string PlayerName { get; set; }
        public ICollection<GameStatistic> GameStatistics { get; set; }
    }
}

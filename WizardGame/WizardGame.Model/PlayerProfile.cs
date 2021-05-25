using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WizardGame.Model
{
    public class PlayerProfile : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        [Key]
        public int Id { get; set; }
        private string playerName;
        [Required]
        public string PlayerName
        {
            get
            {
                return playerName;
            }
            set
            {
                playerName = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(PlayerName)));
            }
        }

        public bool IsSelected { get; set; } = false;

        // Navigation Properties
        public ICollection<GameStatistic> GameStatistics { get; set; }
    }
}

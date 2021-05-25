using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WizardGame.Model
{
    public class PlayerProfile : INotifyPropertyChanged
    {
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
        public ICollection<GameStatistic> GameStatistics { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}

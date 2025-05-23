﻿using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WizardGame.Model
{
    public class Player : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        [Key]
        public int Id { get; set; }
        private string playerName;
        [Required]
        public string PlayerName
        {
            get => playerName;
            set
            {
                playerName = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(PlayerName)));
            }
        }

        public bool IsSelected { get; set; } = false;

        // Navigation Properties
        public ICollection<GameData> GameData { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using WizardGame.App.Core.Services;
using WizardGame.App.Helpers;
using WizardGame.Model;

namespace WizardGame.App.ViewModels
{
    public class PlayerProfilePageViewModel : Observable
    {
        private ObservableCollection<PlayerProfile> playerProfiles;
        public ObservableCollection<PlayerProfile> PlayerProfiles
        {
            get => playerProfiles;
            set
            {
                playerProfiles = value;
                OnPropertyChanged("PlayerProfiles");
            }
        }
        private readonly HttpDataService dataService = new HttpDataService("http://localhost:34367");

        // Constructor

        public PlayerProfilePageViewModel()
        {
            PlayerProfiles = new ObservableCollection<PlayerProfile>();
        }

        // CRUD Operations

        internal async Task LoadAllPlayerProfilesAsync()
        {
            IEnumerable<PlayerProfile> playerProfiles = await dataService.GetAsync<IEnumerable<PlayerProfile>>("api/PlayerProfiles");

            if (playerProfiles != null)
            {
                PlayerProfiles.Clear();

                foreach (PlayerProfile p in playerProfiles)
                {
                    PlayerProfiles.Add(p);
                }
            }
        }

        internal async Task AddNewPlayerProfileAsync(string playerName)
        {
            PlayerProfile playerProfile = new PlayerProfile()
            {
                PlayerName = playerName,
                GameStatistics = null
            };

            await dataService.PostAsJsonAsync("api/PlayerProfiles", playerProfile);
        }

        internal async Task DeletePlayerProfileAsync(int id)
        {
            await dataService.DeleteAsync($"api/PlayerProfiles/{id}");
        }

        internal async Task UpdatePlayerProfileAsync(int id, string newName)
        {
            PlayerProfile playerProfile = new PlayerProfile()
            {
                Id = id,
                PlayerName = newName
            };

            await dataService.PutAsJsonAsync($"api/PlayerProfiles/{id}", playerProfile);
        }
    }
}

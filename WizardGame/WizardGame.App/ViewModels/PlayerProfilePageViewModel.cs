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
        private ObservableCollection<PlayerProfile> playerProfiles = new ObservableCollection<PlayerProfile>();
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
        public static PlayerProfile SelectedPlayer { get; set; }


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

            // Add to database
            await dataService.PostAsJsonAsync("api/PlayerProfiles", playerProfile);

            // Add to ObservableCollection
            PlayerProfiles.Add(playerProfile);
        }

        internal async Task DeletePlayerProfileAsync(int id)
        {
            // Remove from database
            await dataService.DeleteAsync($"api/PlayerProfiles/{id}");

            // Remove from ObservableCollection
            foreach (PlayerProfile p in PlayerProfiles)
            {
                if (p.Id == id)
                {
                    PlayerProfiles.Remove(p);
                    return;
                }
            }
        }

        internal async Task UpdatePlayerProfileAsync(int id, string newName)
        {
            PlayerProfile playerProfile = new PlayerProfile()
            {
                Id = id,
                PlayerName = newName
            };

            // Update in database
            await dataService.PutAsJsonAsync($"api/PlayerProfiles/{id}", playerProfile);

            // Update in ObservableCollection
            for (int i = 0; i < PlayerProfiles.Count; i++)
            {
                if (PlayerProfiles[i].Id == id)
                {
                    PlayerProfiles[i] = playerProfile;
                }
            }
        }
    }
}

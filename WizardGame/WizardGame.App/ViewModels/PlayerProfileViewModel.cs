using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using WizardGame.App.Core.Services;
using WizardGame.App.Core.Helpers;
using WizardGame.App.Helpers;
using WizardGame.Model;
using System.Linq;

namespace WizardGame.App.ViewModels
{
    public class PlayerProfileViewModel : Observable
    {
        private readonly HttpDataService dataService = new HttpDataService("http://localhost:34367");
        public static EventHandler SelectedPlayerChangedEvent;

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
        private ObservableCollection<PlayerProfile> topPlayers = new ObservableCollection<PlayerProfile>();
        public ObservableCollection<PlayerProfile> TopPlayers
        {
            get => topPlayers;
            set
            {
                topPlayers = value;
                OnPropertyChanged("TopPlayers");
            }
        }
        private PlayerProfile selectedPlayer;
        public PlayerProfile SelectedPlayer
        {
            get => selectedPlayer;
            set
            {
                selectedPlayer = value;
                OnPropertyChanged("SelectedPlayer");
            }
        }

        

        // CRUD Operations
        internal async Task LoadAllPlayerProfilesAsync()
        {
            try
            {
                PlayerProfiles = await dataService.GetAsync<ObservableCollection<PlayerProfile>>("api/PlayerProfiles");
            }
            catch (Exception e)
            {
                PlayerProfiles = new ObservableCollection<PlayerProfile>();
                Console.WriteLine(e.StackTrace);
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

        internal async Task LoadSelectedPlayerAsync()
        {
            try
            {
                SelectedPlayer = await dataService.GetAsync<PlayerProfile>("api/PlayerProfiles/Selected");
            }
            catch (Exception e)
            {
                SelectedPlayer = new PlayerProfile()
                {
                    Id = 0,
                    PlayerName = "Deleted",
                    IsSelected = true,
                    GameStatistics = null
                };
                Console.WriteLine(e.StackTrace);
            }
        }
            

        internal async Task SetSelectedPlayerAsync(int id)
        {
            PlayerProfile profile;

            for (int i = 0; i < PlayerProfiles.Count; i++)
            {
                profile = PlayerProfiles[i];
                profile.IsSelected = false;

                if (profile.Id == id)
                {
                    profile.IsSelected = true;
                    SelectedPlayer = profile;
                }

                // Update in ObservableCollection
                PlayerProfiles[i] = profile;

                // Update in database
                await dataService.PutAsJsonAsync($"api/PlayerProfiles/{profile.Id}", profile);
            }
        }
    }
}

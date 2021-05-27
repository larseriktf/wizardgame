using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using WizardGame.App.Core.Services;
using WizardGame.App.Helpers;
using WizardGame.Model;

namespace WizardGame.App.ViewModels
{
    public class PlayerProfileViewModel : Observable
    {
        private readonly HttpDataService dataService = new HttpDataService("http://localhost:34367");
        public static EventHandler SelectedPlayerChangedEvent;

        private ObservableCollection<PlayerProfile> player = new ObservableCollection<PlayerProfile>();
        public ObservableCollection<PlayerProfile> Players
        {
            get => player;
            set
            {
                player = value;
                OnPropertyChanged("Players");
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

        // API Operations
        internal async Task LoadAllPlayersAsync()
        {
            try
            {
                Players = await dataService.GetAsync<ObservableCollection<PlayerProfile>>("api/PlayerProfiles");
            }
            catch (Exception e)
            {
                Players = new ObservableCollection<PlayerProfile>();
                Console.WriteLine(e.StackTrace);
            }

        }

        internal async Task AddNewPlayerAsync(string name)
        {
            PlayerProfile profile = new PlayerProfile()
            {
                PlayerName = name,
                GameStatistics = null
            };

            // Add to database
            await dataService.PostAsJsonAsync("api/PlayerProfiles", profile);

            // Add to ObservableCollection
            Players.Add(profile);
        }

        internal async Task DeletePlayerAsync(int id)
        {
            // Remove from database
            await dataService.DeleteAsync($"api/PlayerProfiles/{id}");

            // Remove from ObservableCollection
            foreach (PlayerProfile p in Players)
            {
                if (p.Id == id)
                {
                    Players.Remove(p);
                    return;
                }
            }
        }

        internal async Task UpdatePlayerAsync(int id, string newName)
        {
            PlayerProfile playerProfile = new PlayerProfile()
            {
                Id = id,
                PlayerName = newName
            };

            // Update in database
            await dataService.PutAsJsonAsync($"api/PlayerProfiles/{id}", playerProfile);

            // Update in ObservableCollection
            for (int i = 0; i < Players.Count; i++)
            {
                if (Players[i].Id == id)
                {
                    Players[i] = playerProfile;
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

            for (int i = 0; i < Players.Count; i++)
            {
                profile = Players[i];
                profile.IsSelected = false;

                if (profile.Id == id)
                {
                    profile.IsSelected = true;
                    SelectedPlayer = profile;
                }

                // Update in ObservableCollection
                Players[i] = profile;

                // Update in database
                await dataService.PutAsJsonAsync($"api/PlayerProfiles/{profile.Id}", profile);
            }
        }
    }
}

using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using WizardGame.App.Core.Services;
using WizardGame.App.Helpers;
using WizardGame.Model;

namespace WizardGame.App.ViewModels
{
    public class PlayerViewModel : Observable
    {
        private readonly HttpDataService dataService = new HttpDataService("http://localhost:34367");
        public static EventHandler SelectedPlayerChangedEvent;

        private ObservableCollection<Player> player = new ObservableCollection<Player>();
        public ObservableCollection<Player> Players
        {
            get => player;
            set
            {
                player = value;
                OnPropertyChanged("Players");
            }
        }
        private Player selectedPlayer;
        public Player SelectedPlayer
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
                Players = await dataService.GetAsync<ObservableCollection<Player>>("api/Players");
            }
            catch (Exception e)
            {
                Players = new ObservableCollection<Player>();
                Console.WriteLine(e.StackTrace);
            }

        }

        internal async Task AddNewPlayerAsync(string name)
        {
            Player profile = new Player()
            {
                PlayerName = name,
                GameData = null
            };

            // Add to database
            await dataService.PostAsJsonAsync("api/Players", profile);

            // Add to ObservableCollection
            Players.Add(profile);
        }

        internal async Task DeletePlayerAsync(int id)
        {
            // Remove from database
            await dataService.DeleteAsync($"api/Players/{id}");

            // Remove from ObservableCollection
            foreach (Player p in Players)
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
            Player playerProfile = new Player()
            {
                Id = id,
                PlayerName = newName
            };

            // Update in database
            await dataService.PutAsJsonAsync($"api/Players/{id}", playerProfile);

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
                SelectedPlayer = await dataService.GetAsync<Player>("api/Players/Selected");
            }
            catch (Exception e)
            {
                SelectedPlayer = new Player()
                {
                    Id = 0,
                    PlayerName = "Unselected",
                    IsSelected = true,
                    GameData = null
                };
                Console.WriteLine(e.StackTrace);
            }
        }


        internal async Task SetSelectedPlayerAsync(int id)
        {
            Player profile;

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
                await dataService.PutAsJsonAsync($"api/Players/{profile.Id}", profile);
            }
        }
    }
}

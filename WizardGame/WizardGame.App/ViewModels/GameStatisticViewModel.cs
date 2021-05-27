using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using WizardGame.App.Core.Services;
using WizardGame.App.Helpers;
using WizardGame.Model;

namespace WizardGame.App.ViewModels
{
    public class GameStatisticViewModel : Observable
    {
        private ObservableCollection<GameStatistic> allGames = new ObservableCollection<GameStatistic>();
        public ObservableCollection<GameStatistic> AllGames
        {
            get => allGames;
            set
            {
                allGames = value;
                OnPropertyChanged("AllGames");
            }
        }

        private ObservableCollection<GameStatistic> playerGames = new ObservableCollection<GameStatistic>();
        public ObservableCollection<GameStatistic> PlayerGames
        {
            get => playerGames;
            set
            {
                playerGames = value;
                OnPropertyChanged("PlayerGames");
            }
        }

        private ObservableCollection<GameStatistic> topGames = new ObservableCollection<GameStatistic>();
        public ObservableCollection<GameStatistic> TopGames
        {
            get => topGames;
            set
            {
                topGames = value;
                OnPropertyChanged("TopGames");
            }
        }

        private readonly HttpDataService dataService = new HttpDataService("http://localhost:34367");


        // CRUD Operations
        internal async Task LoadAllGamesAsync() =>
            AllGames = await dataService.GetAsync<ObservableCollection<GameStatistic>>("api/GameStatistics");

        internal async Task LoadPlayerGamesAsync(int playerId)
        {
            PlayerGames = await dataService.GetAsync<ObservableCollection<GameStatistic>>($"api/GameStatistics/Player/{playerId}");
        }

        internal async Task LoadTopGamesAsync()
        {
            TopGames = await dataService.GetAsync<ObservableCollection<GameStatistic>>("api/GameStatistics/Top");
        }

        internal async Task AddPlayerGameAsync(int playerId, int wavesPlayed, int enemiesDefeated, TimeSpan elapsedTime)
        {
            GameStatistic currentGame = new GameStatistic()
            {
                PlayerProfileId = playerId,
                WavesPlayed = wavesPlayed,
                EnemiesDefeated = enemiesDefeated,
                ElapsedTime = elapsedTime
            };

            await dataService.PostAsJsonAsync("api/GameStatistics", currentGame);
        }

        //internal async Task AddNewPlayerProfileAsync(string playerName)
        //{
        //    PlayerProfile playerProfile = new PlayerProfile()
        //    {
        //        PlayerName = playerName,
        //        GameStatistics = null
        //    };

        //    // Add to database
        //    await dataService.PostAsJsonAsync("api/PlayerProfiles", playerProfile);

        //    // Add to ObservableCollection
        //    PlayerProfiles.Add(playerProfile);
        //}

        //internal async Task DeletePlayerProfileAsync(int id)
        //{
        //    // Remove from database
        //    await dataService.DeleteAsync($"api/PlayerProfiles/{id}");

        //    // Remove from ObservableCollection
        //    foreach (PlayerProfile p in PlayerProfiles)
        //    {
        //        if (p.Id == id)
        //        {
        //            PlayerProfiles.Remove(p);
        //            return;
        //        }
        //    }
        //}

        //internal async Task UpdatePlayerProfileAsync(int id, string newName)
        //{
        //    PlayerProfile playerProfile = new PlayerProfile()
        //    {
        //        Id = id,
        //        PlayerName = newName
        //    };

        //    // Update in database
        //    await dataService.PutAsJsonAsync($"api/PlayerProfiles/{id}", playerProfile);

        //    // Update in ObservableCollection
        //    for (int i = 0; i < PlayerProfiles.Count; i++)
        //    {
        //        if (PlayerProfiles[i].Id == id)
        //        {
        //            PlayerProfiles[i] = playerProfile;
        //        }
        //    }
        //}

        //internal async Task LoadSelectedPlayerAsync()
        //{
        //    IEnumerable<PlayerProfile> playerProfiles = await dataService.GetAsync<IEnumerable<PlayerProfile>>("api/PlayerProfiles");

        //    foreach (PlayerProfile p in playerProfiles)
        //    {
        //        if (p.IsSelected == true)
        //        {
        //            SelectedPlayer = p;
        //        }
        //    }
        //}
    }
}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using WizardGame.App.Core.Services;
using WizardGame.App.Helpers;
using WizardGame.Model;

namespace WizardGame.App.ViewModels
{
    public class GameStatisticViewModel : Observable
    {
        private readonly HttpDataService dataService = new HttpDataService("http://localhost:34367");

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

        // API interactions
        internal async Task LoadAllGamesAsync() =>
            AllGames = await dataService.GetAsync<ObservableCollection<GameStatistic>>("api/GameStatistics");

        internal async Task LoadPlayerGamesAsync(int playerId)
        {
            PlayerGames = await dataService.GetAsync<ObservableCollection<GameStatistic>>($"api/GameStatistics/Player/{playerId}");
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

        internal async Task LoadTopGamesAsync()
        {
            IEnumerable<PlayerProfile> allPlayers = await dataService.GetAsync<ObservableCollection<PlayerProfile>>("api/PlayerProfiles");

            List<GameStatistic> topGames = new List<GameStatistic>();

            // Find the best game for each player and add them to temporary list
            foreach (PlayerProfile player in allPlayers)
            {
                GameStatistic currentBestGame = null;

                // Find the new best game
                foreach (GameStatistic game in player.GameStatistics)
                {
                    if (currentBestGame == null)
                    {
                        currentBestGame = game;
                        break;
                    }

                    if (game.WavesPlayed >= currentBestGame.WavesPlayed)
                    {
                        currentBestGame = game;
                    }
                }
                topGames.Add(currentBestGame);
            }
            topGames.OrderBy(g => g.WavesPlayed);

            // Add games to list
            foreach (GameStatistic game in topGames)
            {
                TopGames.Add(game);
            }
            
        }
    }
}

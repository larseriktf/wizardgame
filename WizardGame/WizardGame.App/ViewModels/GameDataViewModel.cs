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
    public class GameDataViewModel : Observable
    {
        private readonly HttpDataService dataService = new HttpDataService("http://localhost:34367");

        private ObservableCollection<GameData> allGames = new ObservableCollection<GameData>();
        public ObservableCollection<GameData> AllGames
        {
            get => allGames;
            set
            {
                allGames = value;
                OnPropertyChanged("AllGames");
            }
        }

        private ObservableCollection<GameData> playerGames = new ObservableCollection<GameData>();
        public ObservableCollection<GameData> PlayerGames
        {
            get => playerGames;
            set
            {
                playerGames = value;
                OnPropertyChanged("PlayerGames");
            }
        }

        private ObservableCollection<GameData> topGames = new ObservableCollection<GameData>();
        public ObservableCollection<GameData> TopGames
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
            AllGames = await dataService.GetAsync<ObservableCollection<GameData>>("api/GameData");

        internal async Task LoadPlayerGamesAsync(int playerId)
        {
            PlayerGames = await dataService.GetAsync<ObservableCollection<GameData>>($"api/GameData/Player/{playerId}");
        }

        internal async Task AddPlayerGameAsync(int playerId, int wavesPlayed, int enemiesDefeated, TimeSpan elapsedTime)
        {
            GameData currentGame = new GameData()
            {
                PlayerId = playerId,
                WavesPlayed = wavesPlayed,
                EnemiesDefeated = enemiesDefeated,
                ElapsedTime = elapsedTime
            };

            await dataService.PostAsJsonAsync("api/GameData", currentGame);
        }

        internal async Task LoadTopGamesAsync()
        {
            IEnumerable<Player> allPlayers = await dataService.GetAsync<ObservableCollection<Player>>("api/Players");

            List<GameData> topGames = new List<GameData>();

            // Find the best game for each player and add them to temporary list
            foreach (Player player in allPlayers)
            {
                GameData currentBestGame = null;

                // Find the new best game
                foreach (GameData game in player.GameData)
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
            foreach (GameData game in topGames)
            {
                TopGames.Add(game);
            }

        }
    }
}

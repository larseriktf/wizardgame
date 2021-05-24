using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using WizardGame.App.Core.Services;
using WizardGame.App.Helpers;
using WizardGame.Model;

namespace WizardGame.App.ViewModels
{
    public class PlayerProfilePageViewModel : Observable
    {
        public ObservableCollection<PlayerProfile> PlayerProfiles { get; set; } = new ObservableCollection<PlayerProfile>();
        private HttpDataService dataService = new HttpDataService();
        private string basePath = "http://localhost:34367/";

        internal async Task LoadAllPlayerProfilesAsync()
        {
            IEnumerable<PlayerProfile> profiles = await dataService.GetAsync<IEnumerable<PlayerProfile>>($"{basePath}api/PlayerProfiles");

            foreach (PlayerProfile profile in profiles)
            {
                PlayerProfiles.Add(profile);
            }
        }

        internal async Task AddNewPlayerProfileAsync(string playerName)
        {
            PlayerProfile playerProfile = new PlayerProfile()
            {
                PlayerName = playerName,
                GameStatistics = null
            };

            await dataService.PostAsJsonAsync($"{basePath}api/PlayerProfiles", playerProfile);
        }

        internal async Task DeletePlayerProfileAsync(int Id)
        {
            await dataService.DeleteAsync($"{basePath}api/PlayerProfiles/{Id}");
        }
    }
}

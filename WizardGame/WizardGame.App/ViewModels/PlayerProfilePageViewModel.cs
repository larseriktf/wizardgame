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

        internal async Task LoadAllPlayerProfilesAsync()
        {
            List<PlayerProfile> configurations = new List<PlayerProfile>()
            {
                await dataService.GetAsync<PlayerProfile>("http://localhost:34367/api/PlayerProfiles/1"),
                await dataService.GetAsync<PlayerProfile>("http://localhost:34367/api/PlayerProfiles/2"),
                await dataService.GetAsync<PlayerProfile>("http://localhost:34367/api/PlayerProfiles/3")
            };

            foreach (PlayerProfile playerProfiles in configurations)
            {   // Add configurations to observable collection
                PlayerProfiles.Add(playerProfiles);
            }
        }

        internal async Task AddNewPlayerProfileAsync(string playerName)
        {
            PlayerProfile playerProfile = new PlayerProfile()
            {
                PlayerName = "playerName"
            };

            await dataService.PostAsync<PlayerProfile>("http://localhost:34367/api/PlayerProfiles/", playerProfile);
        }
    }
}

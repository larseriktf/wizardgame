using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using WizardGame.App.Core.Services;
using WizardGame.App.Helpers;
using WizardGame.Model;

namespace WizardGame.App.ViewModels
{
    public class GamePageViewModel : Observable
    {
        private HttpDataService dataService = new HttpDataService();
        public ObservableCollection<Configuration> Configurations { get; set; } = new ObservableCollection<Configuration>();
        public Configuration Configuration { get; set; }

        internal async Task LoadSpecificConfigurationAsync(int? Id = 0)
        {
            if (Id == null)
            {
                return;
            }
            Configuration = await dataService.GetAsync<Configuration>("http://localhost:34367/api/Configurations/" + Id);
        }

        internal async Task LoadAllConfigurationsAsync()
        {
            List<Configuration> configurations = new List<Configuration>()
            {
                await dataService.GetAsync<Configuration>("http://localhost:34367/api/Configurations/1"),
                await dataService.GetAsync<Configuration>("http://localhost:34367/api/Configurations/2"),
                await dataService.GetAsync<Configuration>("http://localhost:34367/api/Configurations/3"),
                await dataService.GetAsync<Configuration>("http://localhost:34367/api/Configurations/4"),
                await dataService.GetAsync<Configuration>("http://localhost:34367/api/Configurations/5"),
                await dataService.GetAsync<Configuration>("http://localhost:34367/api/Configurations/6"),
            };

            foreach (Configuration configuration in configurations)
            {   // Add configurations to observable collection
                Configurations.Add(configuration);
            }
        }
    }
}

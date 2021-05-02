
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using WizardGame.App.DataAccess;
using WizardGame.App.Helpers;
using WizardGame.Model;

namespace WizardGame.App.ViewModels
{
    public class GamePageViewModel : Observable
    {
        public ObservableCollection<Configuration> Configurations { get; set; } = new ObservableCollection<Configuration>();

        internal async Task LoadConfigurationsAsync()
        {
            var configurations = await ConfigurationsDataAccess.GetConfigurationsAsync();

            foreach (Configuration configuration in configurations)
            {   // Add configurations to observable collection
                Configurations.Add(configuration);
            }
        }
    }
}

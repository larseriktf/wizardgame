using WizardGame.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace WizardGame.App.DataAccess
{
    public static class ConfigurationsDataAccess
    {
        private static readonly HttpClient httpClient = new HttpClient();
        private static readonly Uri coursesUri = new Uri("http://localhost:34367/api/Game");


        public static async Task<Configuration[]> GetConfigurationsAsync()
        {
            HttpResponseMessage result = await httpClient.GetAsync(coursesUri);
            string json = await result.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<Configuration[]>(json);
        }
    }
}

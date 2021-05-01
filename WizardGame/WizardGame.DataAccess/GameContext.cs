using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using WizardGame.Model;

namespace WizardGame.DataAccess
{
    public class GameContext : DbContext
    {
        public DbSet<Configuration> Configurations { get; set; }

        public GameContext(DbContextOptions<GameContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // New Connection Method

            // For Donau
            //SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder
            //{
            //    DataSource = @"donau.hiof.no",
            //    InitialCatalog = "lefaber",
            //    PersistSecurityInfo = true,
            //    UserID = "lefaber",
            //    Password = "tgJjs\"2d"
            //};

            // For local
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder
            {
                DataSource = @"(localdb)\MSSQLLocalDB",
                InitialCatalog = "Game.Database",
                IntegratedSecurity = true
            };

            optionsBuilder.UseSqlServer(builder.ConnectionString.ToString());
        }
    }
}

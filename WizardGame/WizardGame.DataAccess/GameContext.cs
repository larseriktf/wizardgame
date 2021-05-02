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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Configuration>()
                .HasData(
                new Configuration()
                {
                    Id = 1,
                    ConfigurationName = "Default Configuration",
                    
                    Volume = 50,
                    DisplayMode = 0,

                    NavContinue = "ENTER",
                    NavPause = "ESCAPE",
                    NavBack = "BACKSPACE",

                    MoveLeft = "A",
                    MoveUp = "W",
                    MoveRight = "D",
                    MoveDown = "S",

                    Action1 = "LEFTARROW",
                    Action2 = "UPARROW",
                    Action3 = "RIGHTARROW",
                    Action4 = "DOWNARROW",

                    Interact1 = "R",
                    Interact2 = "F",
                    Interact3 = "C"
                },
                new Configuration()
                {
                    Id = 2,
                    ConfigurationName = "Switch",

                    Volume = 50,
                    DisplayMode = 0,

                    NavContinue = "ENTER",
                    NavPause = "ESCAPE",
                    NavBack = "BACKSPACE",

                    MoveLeft = "LEFTARROW",
                    MoveDown = "UPARROW",
                    MoveRight = "RIGHTARROW",
                    MoveUp = "DOWNARROW",

                    Action1 = "A",
                    Action2 = "W",
                    Action3 = "D",
                    Action4 = "S",

                    Interact1 = "R",
                    Interact2 = "F",
                    Interact3 = "C"
                },
                new Configuration()
                {
                    Id = 3,
                    ConfigurationName = "Windowed Borderless",

                    Volume = 50,
                    DisplayMode = 1,

                    NavContinue = "ENTER",
                    NavPause = "ESCAPE",
                    NavBack = "BACKSPACE",

                    MoveLeft = "A",
                    MoveUp = "W",
                    MoveRight = "D",
                    MoveDown = "S",

                    Action1 = "LEFTARROW",
                    Action2 = "UPARROW",
                    Action3 = "RIGHTARROW",
                    Action4 = "DOWNARROW",

                    Interact1 = "R",
                    Interact2 = "F",
                    Interact3 = "C"
                },
                new Configuration()
                {
                    Id = 4,
                    ConfigurationName = "Fullscreen",

                    Volume = 50,
                    DisplayMode = 2,

                    NavContinue = "ENTER",
                    NavPause = "ESCAPE",
                    NavBack = "BACKSPACE",

                    MoveLeft = "A",
                    MoveUp = "W",
                    MoveRight = "D",
                    MoveDown = "S",

                    Action1 = "LEFTARROW",
                    Action2 = "UPARROW",
                    Action3 = "RIGHTARROW",
                    Action4 = "DOWNARROW",

                    Interact1 = "R",
                    Interact2 = "F",
                    Interact3 = "C"
                },
                new Configuration()
                {
                    Id = 5,
                    ConfigurationName = "NumPad",

                    Volume = 50,
                    DisplayMode = 0,

                    NavContinue = "ENTER",
                    NavPause = "ESCAPE",
                    NavBack = "BACKSPACE",

                    MoveLeft = "A",
                    MoveUp = "W",
                    MoveRight = "D",
                    MoveDown = "S",

                    Action1 = "NUMLEFT",
                    Action2 = "NUMUP",
                    Action3 = "NUMRIGHT",
                    Action4 = "NUMDOWN",

                    Interact1 = "R",
                    Interact2 = "F",
                    Interact3 = "C"
                },
                new Configuration()
                {
                    Id = 6,
                    ConfigurationName = "Close Together",

                    Volume = 50,
                    DisplayMode = 0,

                    NavContinue = "ENTER",
                    NavPause = "ESCAPE",
                    NavBack = "BACKSPACE",

                    MoveLeft = "A",
                    MoveUp = "W",
                    MoveRight = "D",
                    MoveDown = "S",

                    Action1 = "N",
                    Action2 = "J",
                    Action3 = "K",
                    Action4 = "M",

                    Interact1 = "R",
                    Interact2 = "F",
                    Interact3 = "C"
                });
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

using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using WizardGame.Model;

namespace WizardGame.DataAccess
{
    public class GameContext : DbContext
    {
        public DbSet<Configuration> Configurations { get; set; }
        public DbSet<GameStatistic> GameStatistics { get; set; }
        public DbSet<PlayerProfile> PlayerProfiles { get; set; }

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

            modelBuilder.Entity<GameStatistic>()
                .HasData(
                new GameStatistic()
                {
                    Id = 1,
                    WavesPlayed = 10
                },
                new GameStatistic()
                {
                    Id = 2,
                    WavesPlayed = 10
                },
                new GameStatistic()
                {
                    Id = 3,
                    WavesPlayed = 10
                },
                new GameStatistic()
                {
                    Id = 4,
                    WavesPlayed = 10
                },
                new GameStatistic()
                {
                    Id = 5,
                    WavesPlayed = 10
                },
                new GameStatistic()
                {
                    Id = 6,
                    WavesPlayed = 10
                },
                new GameStatistic()
                {
                    Id = 7,
                    WavesPlayed = 10
                },
                new GameStatistic()
                {
                    Id = 8,
                    WavesPlayed = 10
                },
                new GameStatistic()
                {
                    Id = 9,
                    WavesPlayed = 10
                });

            modelBuilder.Entity<PlayerProfile>()
                .HasData(
                new PlayerProfile()
                {
                    Id = 1,
                    PlayerName = "Åge",
                },
                new PlayerProfile()
                {
                    Id = 2,
                    PlayerName = "Patrenko Escobar",
                },
                new PlayerProfile()
                {
                    Id = 3,
                    PlayerName = "Player3",
                });
        }
    }
}

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
                    WavesPlayed = 1,
                    PlayerProfileId = 1
                },
                new GameStatistic()
                {
                    Id = 2,
                    WavesPlayed = 14,
                    PlayerProfileId = 1
                },
                new GameStatistic()
                {
                    Id = 3,
                    WavesPlayed = 29,
                    PlayerProfileId = 1
                },
                new GameStatistic()
                {
                    Id = 4,
                    WavesPlayed = 5,
                    PlayerProfileId = 1
                },
                new GameStatistic()
                {
                    Id = 5,
                    WavesPlayed = 15,
                    PlayerProfileId = 1
                },
                new GameStatistic()
                {
                    Id = 6,
                    WavesPlayed = 2,
                    PlayerProfileId = 2
                },
                new GameStatistic()
                {
                    Id = 7,
                    WavesPlayed = 40,
                    PlayerProfileId = 2
                },
                new GameStatistic()
                {
                    Id = 8,
                    WavesPlayed = 28,
                    PlayerProfileId = 2
                },
                new GameStatistic()
                {
                    Id = 9,
                    WavesPlayed = 10,
                    PlayerProfileId = 2
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

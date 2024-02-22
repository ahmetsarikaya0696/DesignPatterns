namespace StrategyDesingPattern.Models
{
    public class Settings
    {
        public const string claimDatabaseType = "databasetype";
        public EDatabaseType DatabaseType { get; set; } = EDatabaseType.SqlServer;
    }
}

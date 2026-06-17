
namespace Core.Configs
{
    public static class ConfigsSettings
    {
        private const string PlayerConfigName = "Configs/playerConfig";
        private const string ProjectilesConfigName = "Configs/projectilesConfig";
        private const string AsteroidsConfigName = "Configs/asteroidsConfig";
        private const string EnemiesConfigName = "Configs/enemiesConfig";

        public static string GetConfigName<T>()
        {
            var classType = typeof(T);
            if (classType == typeof(AsteroidsData))
            {
                return AsteroidsConfigName;
            }
            
            if (classType == typeof(ProjectilesData))
            {
                return ProjectilesConfigName;
            }
            
            if (classType == typeof(PlayerData))
            {
                return PlayerConfigName;
            }
            
            if (classType == typeof(EnemiesData))
            {
                return EnemiesConfigName;
            }
            
            return string.Empty;
        }
        
    }
}
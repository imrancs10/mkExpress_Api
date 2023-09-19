namespace MKExpress.API.Config
{
    public static class ConfigManager
    {
        public static IConfiguration AppSetting
        {
            get;
        }

        static ConfigManager()
        {
            AppSetting = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
        }
    }
}

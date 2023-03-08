namespace uServiceDemo.Infrastructure
{
    class Constants
    {
        public static class Database
        {
            public const string DabaseConnectionString = "uServiceDemoDB";

            public static class Tables
            {
                public const string WeatherForecastTable = "WeatherForecast";
            }

            public static class MigrationAssembly
            {
                public const string Sqlite = "uServiceDemo.Infrastructure.Migrations.Sqlite";
                public const string Postgresql = "uServiceDemo.Infrastructure.Migrations.Postgresql";
            }
        }
    }
}

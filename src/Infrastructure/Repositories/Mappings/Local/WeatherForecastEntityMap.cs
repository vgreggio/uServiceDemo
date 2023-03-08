using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using uServiceDemo.Domain.Entities;

namespace uServiceDemo.Infrastructure.Repositories.Mappings.Local
{
    static class LocalWeatherForecastEntityMap
    {
        public static void Map(EntityTypeBuilder<WeatherForecastEntity> entityTypeBuilder)
        {
            entityTypeBuilder.ToTable(Constants.Database.Tables.WeatherForecastTable);

            entityTypeBuilder.HasKey(x => x.Id);
            entityTypeBuilder.Property(x => x.Created).ValueGeneratedOnAdd().HasDefaultValueSql("DATETIME('now')");
            entityTypeBuilder.Property(x => x.LastUpdated).ValueGeneratedOnAddOrUpdate().HasDefaultValueSql("DATETIME('now')");
            entityTypeBuilder.Property(x => x.Version).IsRowVersion();

        }
    }
}

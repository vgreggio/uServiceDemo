using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using uServiceDemo.Domain.Entities;

namespace uServiceDemo.Infrastructure.Repositories.Mappings
{
    static class WeatherForecastEntityMap
    {
        public static void Map(EntityTypeBuilder<WeatherForecastEntity> entityTypeBuilder)
        {
            entityTypeBuilder.ToTable(Constants.Database.Tables.WeatherForecastTable);

            entityTypeBuilder.HasKey(x => x.Id);
            entityTypeBuilder.Property(x => x.Created).ValueGeneratedOnAdd().HasDefaultValueSql("UTC_TIMESTAMP()");
            entityTypeBuilder.Property(x => x.LastUpdated).ValueGeneratedOnAddOrUpdate().HasDefaultValueSql("UTC_TIMESTAMP()");
            entityTypeBuilder.Property(x => x.Version).IsRowVersion();

        }
    }
}

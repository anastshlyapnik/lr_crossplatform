using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;

namespace Shlyapnikova_lr.Data
{
    public class Shlyapnikova_lrContextFactory : IDesignTimeDbContextFactory<Shlyapnikova_lrContext>
    {
        public Shlyapnikova_lrContext CreateDbContext(string[] args)
        {
            // Загружаем настройки из файла appsettings.json
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())  // Указываем базовую директорию для поиска конфигурации
                .AddJsonFile("appsettings.json")  // Загружаем настройки из файла appsettings.json
                .Build();

            // Получаем строку подключения для контекста
            var connectionString = configuration.GetConnectionString("Shlyapnikova_lrContext");


            // Строим и конфигурируем DbContextOptions для контекста
            var optionsBuilder = new DbContextOptionsBuilder<Shlyapnikova_lrContext>();
            optionsBuilder.UseNpgsql(connectionString);     // Указываем использование SQL Server с соответствующей строкой подключения

            // Возвращаем новый экземпляр контекста с конфигурированными параметрами
            return new Shlyapnikova_lrContext(optionsBuilder.Options);
        }
    }
}

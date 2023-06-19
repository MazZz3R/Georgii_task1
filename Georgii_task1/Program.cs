using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MainDatabase;

namespace Georgii_task1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // logger
            NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
            logger.Info("Program started");

            // import settings
            var builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory());
            builder.AddJsonFile("dbsettings.json");
            IConfigurationRoot config = builder.Build();

            string connectionString = config.GetConnectionString("DefaultConnection")!;
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();
            DbContextOptions<ApplicationContext> options = optionsBuilder.UseSqlite(connectionString).Options;


            var context = new ApplicationContext(options);
            var usersRepository = new UsersRepository(context, logger);

            usersRepository.Create(new User { Name = "Ben", Email = "ben2@email.ru", Age = 52, Password = "I KNOW IT SHOULD BE HASHED"});
            usersRepository.Save();

            usersRepository.GetList().ToList().ForEach(user => Console.WriteLine($"{user.Id}. {user.Name} - {user.Age}"));
            Console.Read();
        }
    }
}
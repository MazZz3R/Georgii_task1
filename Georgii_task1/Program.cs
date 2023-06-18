namespace Georgii_task1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var logger = NLog.LogManager.GetCurrentClassLogger();
            logger.Info("Program started");
            var context = new ApplicationContext();
            var usersRepository = new UsersRepository(context, logger);

            usersRepository.Create(new User { Name = "Ben", Email = "ben@email.ru", Age = 41, Password = "I KNOW IT SHOULD BE HASHED"});
            usersRepository.Save();

            usersRepository.GetList().ToList().ForEach(user => Console.WriteLine($"{user.Id}. {user.Name} - {user.Age}"));
            Console.Read();
        }
    }
}
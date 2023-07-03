using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MainDatabase
{
    public class UsersRepository : IRepository<User>
    {
        private readonly ILogger _logger;
        private ApplicationContext db;

        /// <summary>
        /// Repository for users table
        /// </summary>
        /// <param name="context">ApplicationContext for users table</param>
        /// <param name="logger">Logger</param>
        public UsersRepository(ApplicationContext context, ILogger<UsersRepository> logger)
        {
            _logger = logger;
            _logger.LogInformation("Creating UsersRepository");
            db = context;
        }
        public UsersRepository(ILogger<UsersRepository> logger)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();

            DbContextOptions<ApplicationContext> options = optionsBuilder.UseSqlite("Data Source=D:\\helloapp.db").Options;
            db = new ApplicationContext(options);
            _logger = logger;
        }

        /// <summary>
        /// Returns all users
        /// </summary>
        /// <returns>IEnumerable of all users</returns>
        public IEnumerable<User> GetList()
        {
            _logger.LogInformation("Getting all users");

            var users = db.Users.Include(p => p.followers).Include(p => p.following);

            return users;
        }

        /// <summary>
        /// Returns user with specified id
        /// </summary>
        /// <param name="id">Id of user to return</param>
        /// <returns>User with specified id</returns>
        /// <exception cref="System.InvalidOperationException">
        /// Thrown when user with specified id not found
        /// </exception>
        public User Get(int id)
        {
            _logger.LogInformation($"Getting user with id {id}");

            try
            {
                return db.Users.Include(p => p.followers).Include(p => p.following).First(p => p.Id == id);
            }
            catch (System.InvalidOperationException)
            {
                _logger.LogError($"User with id {id} not found");
                throw new System.InvalidOperationException($"User with id {id} not found");
            }
        }

        /// <summary>
        /// Adds user to database
        /// </summary>
        /// <param name="user">User to add to database</param>
        public void Create(User user)
        {
            _logger.LogInformation($"Adding user {user.Name}");
            try
            {
                db.Users.Add(new User { Name = user.Name, Email = user.Email, Age = user.Age, Password = user.Password });
            }
            catch (System.InvalidOperationException)
            {
                _logger.LogError($"User with Email {user.Email} already exists");
                throw new System.InvalidOperationException($"User with Email {user.Email} already exists");
            }   

        }

        /// <summary>
        /// Updates user in database
        /// </summary>
        /// <param name="user">User to update in database</param>
        /// <exception cref="System.InvalidOperationException">
        /// Thrown when user with specified id not found
        /// </exception>
        public void Update(User user)
        {
            _logger.LogInformation($"Updating user with id {user.Id}");
            try
            {
                db.Users.Update(user);
            }
            catch (System.InvalidOperationException)
            {
                _logger.LogError($"User with id {user.Id} not found");
                throw new System.InvalidOperationException($"User with id {user.Id} not found");
            }
        }

        /// <summary>
        /// Deletes user with specified id from database
        /// </summary>
        /// <param name="id">
        /// Id of user to delete from database
        /// </param>
        /// <exception cref="System.InvalidOperationException">
        /// Thrown when user with specified id not found
        /// </exception>
        public void Delete(int id)
        {
            _logger.LogInformation($"Deleting user with id {id}");
            try
            {
                var user_db = db.Users.First(p => p.Id == id);
                db.Users.Remove(user_db);
            }
            catch (System.InvalidOperationException)
            {
                _logger.LogError($"User with id {id} not found");
                throw new System.InvalidOperationException($"User with id {id} not found");
            }
        }
        /// <summary>
        /// Saves changes to database
        /// </summary>
        public void Save()
        {
            _logger.LogInformation("Saving changes");
            db.SaveChanges();
        }
        private bool disposed = false;

        /// <summary>
        /// Disposes repository
        /// </summary>
        /// <param name="disposing">
        /// True if called from Dispose(), false if called from destructor
        /// </param>
        public virtual void Dispose(bool disposing)
        {
            if(!this.disposed)
            {
                if(disposing)
                {
                    db.Dispose();
                }
                this.disposed = true;
            }
        }
        /// <summary>
        /// Disposes repository
        /// </summary>
        public void Dispose()
        {
            _logger.LogInformation("Disposing UsersRepository");
            Dispose(true);
            System.GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Adds follower to user
        /// </summary>
        /// <param name="user">
        /// User to add follower to
        /// </param>
        /// <param name="follower">
        /// Follower to add to user
        /// </param>
        public void Follow(User user, User follower)
        {
            _logger.LogInformation($"User {user.Name} is now followed by {follower.Name}");
            user.followers.Add(follower);
        }

        /// <summary>
        /// Removes follower from user
        /// </summary>
        /// <param name="user">
        /// User from remove follower to
        /// </param>
        /// <param name="follower">
        /// Follower to remove from user
        /// </param>
        public void Unfollow(User user, User follower)
        {
            _logger.LogInformation($"User {user.Name} have unfollowed {follower.Name}");
            user.followers.Remove(follower);
        }
    }
}

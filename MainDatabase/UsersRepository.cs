using Microsoft.EntityFrameworkCore;
using NLog;
    
namespace MainDatabase
{
    public class UsersRepository : IRepository<User>
    {
        private readonly NLog.Logger logger;
        private ApplicationContext db;

        /// <summary>
        /// Repository for users table
        /// </summary>
        /// <param name="context">ApplicationContext for users table</param>
        /// <param name="logger">Logger</param>
        public UsersRepository(ApplicationContext context, Logger logger)
        {
            this.logger = logger;
            logger.Info("Creating UsersRepository");
            db = context;
        }

        /// <summary>
        /// Returns all users
        /// </summary>
        /// <returns>IEnumerable of all users</returns>
        public IEnumerable<User> GetList()
        {
            logger.Info("Getting all users");
            var users = (from user in db.Users.Include(p => p.followers).Include(p => p.following)
                            select user).ToList();
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
            logger.Info($"Getting user with id {id}");

            try
            {
                var user_db = (from user in db.Users.Include(p => p.followers).Include(p => p.following)
                               where user.Id == id
                               select user).First();
                return user_db;
            }
            catch (System.InvalidOperationException)
            {
                logger.Error($"User with id {id} not found");
                throw new System.InvalidOperationException($"User with id {id} not found");
            }
        }

        /// <summary>
        /// Adds user to database
        /// </summary>
        /// <param name="user">User to add to database</param>
        public void Create(User user)
        {
            logger.Info($"Adding user {user.Name}");
            db.Users.Add(user);
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
            logger.Info($"Updating user with id {user.Id}");
            try
            {
                db.Users.Update(user);
            }
            catch (System.InvalidOperationException)
            {
                logger.Error($"User with id {user.Id} not found");
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
            logger.Info($"Deleting user with id {id}");
            try
            {
                var user_db = (from user in db.Users.Include(p => p.followers).Include(p => p.following)
                               where user.Id == id
                               select user).First();
                db.Users.Remove(user_db);
            }
            catch (System.InvalidOperationException)
            {
                logger.Error($"User with id {id} not found");
                throw new System.InvalidOperationException($"User with id {id} not found");
            }
        }
        /// <summary>
        /// Saves changes to database
        /// </summary>
        public void Save()
        {
            logger.Info("Saving changes");
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
            logger.Info("Disposing UsersRepository");
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
            logger.Info($"User {user.Name} is now followed by {follower.Name}");
            user.followers.Add(follower);
        }
    }
}

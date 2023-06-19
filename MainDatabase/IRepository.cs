namespace MainDatabase
{
    interface IRepository<T> : IDisposable
        where T : class
    {
        /// <summary>
        /// Get all items
        /// </summary>
        /// <returns>
        /// collection of items
        /// </returns>
        IEnumerable<T> GetList();
        /// <summary>
        /// Get item by id
        /// </summary>
        /// <param name="id">
        /// id of item
        /// </param>
        /// <returns>
        /// item
        /// </returns>
        T Get(int id);
        /// <summary>
        /// Create item
        /// </summary>
        /// <param name="item">
        /// item
        /// </param>
        void Create(T item);
        /// <summary>
        /// Update item
        /// </summary>
        /// <param name="item">
        /// item
        /// </param>
        void Update(T item);
        /// <summary>
        /// Delete item by id
        /// </summary>
        /// <param name="id">
        /// id of item
        /// </param>
        void Delete(int id);
        /// <summary>
        /// Save changes
        /// </summary>
        void Save();
    }
}
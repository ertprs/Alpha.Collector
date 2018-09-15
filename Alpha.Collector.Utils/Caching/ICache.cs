namespace Alpha.Collector.Utils
{
    /// <summary>
    /// Cache manager interface
    /// </summary>
    public interface ICache
    {
        /// <summary>
        /// Gets or sets the value associated with the specified key.
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="key">The key of the value to get.</param>
        /// <returns>The value associated with the specified key.</returns>
        T Get<T>(string key);

        /// <summary>
        /// Adds the specified key and object to the cache.
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="data">Data</param>
        /// <param name="cacheTime">Cache time</param>
        void Add(string key, object data, long cacheTime = 30);

        /// <summary>
        /// Gets a value indicating whether the value associated with the specified key is cached
        /// </summary>
        /// <param name="key">key</param>
        /// <returns>Result</returns>
        bool Contains(string key);

        /// <summary>
        /// Removes the value with the specified key from the cache
        /// </summary>
        /// <param name="key">/key</param>
        void Remove(string key);

        /// <summary>
        /// Clear all cache data
        /// </summary>
        void RemoveAll();

        /// <summary>
        /// Indexer
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        object this[string key] { get; set; }

        /// <summary>
        /// total Count of caching object
        /// </summary>
        int Count { get; }

        /// <summary>
        /// 获取所有Key
        /// </summary>
        /// <returns></returns>
        string GetAllKeys { get; }
    }
}

using System;
using System.Text.RegularExpressions;

namespace Alpha.Collector.Utils
{
    /// <summary>
    /// 缓存到文件
    /// </summary>
    public class FileCache : ICache
    {
        private static string FILE_PATH = AppDomain.CurrentDomain.BaseDirectory + @"\File_Cache\";
        private const string FILE_SUFFIX = ".txt";
        private object locker = new object();

        public FileCache()
        {
            if (!System.IO.Directory.Exists(FILE_PATH))
            {
                System.IO.Directory.CreateDirectory(FILE_PATH);
            }
        }

        /// <summary>
        /// 索引器
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public object this[string key]
        {
            get
            {
                return this.Get<object>(key);
            }
            set
            {
                this.Add(key, value);
            }
        }

        /// <summary>
        /// 获取缓存项的数量
        /// </summary>
        public int Count
        {
            get
            {
                return System.IO.Directory.GetFiles(FILE_PATH).Length;
            }
        }

        public string GetAllKeys
        {
            get
            {
                return "";
            }
        }

        /// <summary>
        /// 添加缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="data"></param>
        /// <param name="cacheTime"></param>
        public void Add(string key, object data, long cacheTime = 30)
        {
            string filePath = this.GetFilePathByKey(key);
            lock (locker)
            {
                if (System.IO.File.Exists(filePath))
                {
                    this.DeleteFile(filePath);
                }

                System.IO.File.AppendAllText(filePath, string.Format("<CacheTime>{0}</CacheTime>", cacheTime) + "\r\n" + data.ToJson());
            }
        }

        /// <summary>
        /// 缓存项是否包含指定键
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool Contains(string key)
        {
            string filePath = this.GetFilePathByKey(key);
            if (!System.IO.File.Exists(filePath))
            {
                return false;
            }

            lock (locker)
            {
                string json = System.IO.File.ReadAllText(filePath);
                bool isExpired = this.CheckExpired(json, key);
                if (isExpired)
                {
                    this.Remove(key);
                }
            }
            return System.IO.File.Exists(filePath);
        }

        /// <summary>
        /// 获取指定键的值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public T Get<T>(string key)
        {
            string filePath = this.GetFilePathByKey(key);
            if (!System.IO.File.Exists(filePath))
            {
                return default(T);
            }

            lock (locker)
            {
                string json = System.IO.File.ReadAllText(filePath);
                try
                {
                    bool isExpired = this.CheckExpired(json, key);

                    if (isExpired)
                    {
                        this.Remove(key);
                        return default(T);
                    }

                    Regex regex = new Regex(@"<CacheTime>([0-9]+)</CacheTime>", RegexOptions.IgnoreCase);
                    json = regex.Replace(json, "").Trim();

                    return json.ToEntity<T>();
                }
                catch (Exception ex)
                {
                    return default(T);
                }
            }
        }

        /// <summary>
        /// 检查是否过期
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        private bool CheckExpired(string json, string key)
        {
            string filePath = this.GetFilePathByKey(key);
            if (!System.IO.File.Exists(filePath))
            {
                return true;
            }

            Regex regex = new Regex(@"<CacheTime>([0-9]+)</CacheTime>", RegexOptions.IgnoreCase);
            Match match = regex.Match(json);
            long cacheTime = match.Success ? Convert.ToInt64(match.Groups[1].Value) : 0;

            System.IO.FileInfo fileInfo = new System.IO.FileInfo(filePath);
            TimeSpan timeSpan = DateTime.Now - fileInfo.LastWriteTime;
            return timeSpan.TotalSeconds >= cacheTime;
        }

        /// <summary>
        /// 移除指定键
        /// </summary>
        /// <param name="key"></param>
        public void Remove(string key)
        {
            string filePath = this.GetFilePathByKey(key);
            this.DeleteFile(filePath);
        }

        /// <summary>
        /// 移除全部缓存
        /// </summary>
        public void RemoveAll()
        {
            string[] filePaths = System.IO.Directory.GetFiles(FILE_PATH);
            foreach (string filePath in filePaths)
            {
                this.DeleteFile(filePath);
            }
        }

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="filePath"></param>
        private void DeleteFile(string filePath)
        {
            if (!System.IO.File.Exists(filePath))
            {
                return;
            }

            try
            {
                lock (locker)
                {
                    System.IO.File.Delete(filePath);
                }
            }
            catch (Exception ex)
            {

            }
        }

        /// <summary>
        /// 根据key获取文件全名
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        private string GetFilePathByKey(string key)
        {
            return FILE_PATH + key + FILE_SUFFIX;
        }
    }
}

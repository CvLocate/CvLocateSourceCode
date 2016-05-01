using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CvLocate.Website.Bl.Common
{
    public class CacheManager
    {
        #region Singleton
        // static holder for instance, need to use lambda to construct since constructor private
        private static readonly Lazy<CacheManager> _instance = new Lazy<CacheManager>(() => new CacheManager());

        // private to prevent direct instantiation.
        private CacheManager()
        {
            Cache = new Dictionary<string, object>();
        }

        // accessor for instance
        public static CacheManager Instance
        {
            get
            {

                return _instance.Value;
            }
        }


        #endregion

        #region Members

        Dictionary<string, object> Cache { get; set; }
        object _locker = new object();

        #endregion


        internal void SaveInCache(CacheItem cacheItem, object data)
        {
            Cache[cacheItem.ToString()] = data;
        }

        internal object GetCacheItem(CacheItem cacheItem)
        {

            if (Cache.ContainsKey(cacheItem.ToString()))
                return Cache[cacheItem.ToString()];
            else
                return null;
        }

        internal void RemoveCacheItem(CacheItem cacheItem)
        {
            Cache.Remove(cacheItem.ToString());
        }

    }
}

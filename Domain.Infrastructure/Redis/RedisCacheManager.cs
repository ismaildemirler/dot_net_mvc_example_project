using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace Domain.Infrastructure.Redis
{
    public class RedisCacheManager<T>
    {
        private readonly IRedisClient _redisClient;
        private readonly string _key;

        public RedisCacheManager(string key) 
        {
            //uzak redis sunucusuyla çalışmak için
            _redisClient = new RedisClient(new Uri(ConfigurationManager.AppSettings["RedisURI"].ToString()));
            _key = key;
        }
       
        public void AddItem(string id, T value, int duration = 24)
        {
            var key = GetItemKey(id);
            _redisClient.Set(key, value);
            _redisClient.ExpireEntryIn(key, TimeSpan.FromHours(duration));
        }

        public T GetItem(string id)
        {
            return _redisClient.Get<T>(GetItemKey(id));
        }

        public List<T> GetItems()
        {
            var keys = _redisClient.ScanAllKeys(string.Format("{0}:*", _key)).ToList();
            return _redisClient.GetValues<T>(keys);
        }

        public void DeleteItem(string id)
        {
            _redisClient.Remove(GetItemKey(id));
        }

        public void UpdateItem(string id, T value)
        {
            AddItem(id, value);
        }

        public void DeleteAllItems()
        {
            var keys = _redisClient.ScanAllKeys(string.Format("{0}:*", _key)).ToList();
            _redisClient.RemoveAll(keys);
        }

        private string GetItemKey(string id)
        {
            return string.Format("{0}:{1}", _key, id);
        }
    }
}

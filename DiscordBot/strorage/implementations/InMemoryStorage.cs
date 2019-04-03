using System;
using System.Collections.Generic;

namespace DiscordBot.strorage.implementations
{
    public class InMemoryStorage : IDataStorage
    {
        private readonly Dictionary<string, object> _dictionary = new Dictionary<string, object>();
        public T RestoreObject<T>(string key)
        {
            if(!_dictionary.ContainsKey(key)) 
                throw new ArgumentException($"Key: {key} was not found");
            
            return (T)_dictionary[key];
        }

        public void StoreObject(object obj, string key)
        {
            if(_dictionary.ContainsKey(key))return;
            _dictionary.Add(key, obj); 
        }
    }
}
using FreshWorksDataStore.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Text;

namespace FreshWorksDataStore.Utilities
{
    internal static class DataStoreUtilities
    {
        internal static bool CheckSize(string value)
        {
            int maxSize = 16 * 1024; //16 KB (1KB=1024 Byte)
            int size = Encoding.ASCII.GetByteCount(value);
            return (size <= maxSize);
        }

        internal static bool IsValidKey(string key)
        {
            return key.Length <= 32;
        }

        internal static bool IsJson(string value)
        {
            try
            {
                JObject.Parse(value);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        internal static bool IsKeyExists(string key, DataStore dataStore)
        {
            return (dataStore?.Records?.FirstOrDefault(item => item.Key == key) != null);
        }
    }
}

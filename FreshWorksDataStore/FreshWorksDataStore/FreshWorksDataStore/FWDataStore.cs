using FreshWorksDataStore.Constants;
using FreshWorksDataStore.Models;
using FreshWorksDataStore.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FreshWorksDataStore
{
    public class FWDataStore
    {        
        static FWDataStore()
        {
            if (!File.Exists(FWConstant.DataStoreFile))
            {                
                using (var file = File.Create(FWConstant.DataStoreFile))
                {
                    
                }
            }
        }

        /// <summary>
        /// Create record in key value data store
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void Create(string key, string value)
        {
            if (FileUtilities.IsFileSizeLessThanOneGB(FWConstant.DataStoreFile))
            {
                XMLUtilities<DataStore> xmlUtilities = new XMLUtilities<DataStore>();
                var dataStore = xmlUtilities.DeSerialize(FWConstant.DataStoreFile);
                if (DataStoreUtilities.IsValidKey(key))
                {
                    if (!DataStoreUtilities.IsKeyExists(key, dataStore))
                    {
                        if (DataStoreUtilities.IsJson(value))
                        {
                            if (DataStoreUtilities.CheckSize(value))
                            {
                                if (dataStore.Records != null)
                                    dataStore.Records.Add(new Record { Key = key, Value = value });
                                else
                                    dataStore.Records = new List<Record> { new Record { Key = key, Value = value } };
                                xmlUtilities.Serialize(dataStore, FWConstant.DataStoreFile);
                            }
                            else
                                throw new Exception("Size of data exceeds the limit of data store");
                        }
                        else
                            throw new Exception("Invalid json content");
                    }
                    else
                        throw new Exception("Key already Exists");
                }
                else
                    throw new Exception("Key size should not exceed 32 characters");
            }
            else
                throw new Exception("Datastore size exceeds 1 GB");
        }

        /// <summary>
        /// Delete the record from data store using key
        /// </summary>
        /// <param name="key"></param>
        public void Delete(string key)
        {
            XMLUtilities<DataStore> xmlUtilities = new XMLUtilities<DataStore>();
            var dataStore = xmlUtilities.DeSerialize(FWConstant.DataStoreFile);
            if (DataStoreUtilities.IsKeyExists(key, dataStore))
            {
                dataStore.Records.Remove(dataStore.Records.FirstOrDefault(item => item.Key == key));                
                xmlUtilities.Serialize(dataStore, FWConstant.DataStoreFile);
            }
            else
                throw new Exception("Invalid key");
        }

        /// <summary>
        /// Used to read the JSON data from datastore using key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string Read(string key)
        {
            XMLUtilities<DataStore> xmlUtilities = new XMLUtilities<DataStore>();
            var dataStore = xmlUtilities.DeSerialize(FWConstant.DataStoreFile);
            if (DataStoreUtilities.IsKeyExists(key, dataStore))
            {
                return dataStore.Records.FirstOrDefault(item => item.Key == key).Value;
            }
            else
                throw new Exception("Invalid key");
        }

        public List<Record> GetAll()
        {
            XMLUtilities<DataStore> xmlUtilities = new XMLUtilities<DataStore>();
            var dataStore = xmlUtilities.DeSerialize(FWConstant.DataStoreFile);
            return dataStore?.Records;
        }
    }
}

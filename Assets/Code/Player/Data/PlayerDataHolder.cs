using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using Code.Enums;
using UnityEngine;
using UnityEngine.Events;

namespace Code.Player.Data
{
    public class PlayerDataHolder : MonoBehaviour
    {
       private PlayerData _playerData;

       public PlayerData PlayerData => _playerData;
        public UnityEvent<ResourceTypeToCount,bool> OnMoneyResourcesChanged { get; private set; }

        public void Awake()
        {
            OnMoneyResourcesChanged = new UnityEvent<ResourceTypeToCount,bool>();
            
            Load();
        }

        public void AddResource(ResourceType type, int count)
        {
            var resource = _playerData.resourcesCounts.FirstOrDefault(t => t.Type == type);

            if (resource == null)
            {
                resource = new ResourceTypeToCount()
                {
                    Count = count,
                    Type = type
                };
                _playerData.resourcesCounts.Add(resource);
                return;
            }
            else
                resource.Count += count;
            

            OnMoneyResourcesChanged.Invoke(resource,true);
        }
        
        public void RemoveResource(ResourceType type, int count)
        {
            var resource = _playerData.resourcesCounts.FirstOrDefault(t => t.Type == type);

            if (resource == null)
            {
                resource = new ResourceTypeToCount()
                {
                    Count = 0,
                    Type = type
                };
                _playerData.resourcesCounts.Add(resource);
                return;
            }
            else
                resource.Count -= count;
            

            OnMoneyResourcesChanged.Invoke(resource,false);
        }
        
        private void OnApplicationQuit()
        {
            Save();
        }

        public void Save()
        {
            var formatter = new BinaryFormatter();
            var path = Application.persistentDataPath + "/player.save";
            var stream = new FileStream(path, FileMode.Create);

            formatter.Serialize(stream, _playerData);
            stream.Close();
        }

        public void Load()
        {
            var path = Application.persistentDataPath + "/player.save";

            if (File.Exists(path))
            {
                var formatter = new BinaryFormatter();
                var stream = new FileStream(path, FileMode.Open);
                
                _playerData = formatter.Deserialize(stream) as PlayerData;
                stream.Close();
            }
            else
            {
                _playerData = new PlayerData()
                {
                    resourcesCounts = new List<ResourceTypeToCount>()
                };
                
                foreach (ResourceType resourceType in Enum.GetValues(typeof(ResourceType)))
                {
                    var resource = new ResourceTypeToCount()
                    {
                        Count = 0,
                        Type = resourceType,
                    };

                    _playerData.resourcesCounts.Add(resource);
                }
            }
        }
    }
}
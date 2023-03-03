using System;
using System.Collections.Generic;
using Code.Enums;
using UnityEngine;

namespace Code.Player.Data
{
    [Serializable]
    public class ResourceTypeToCount
    {
        public ResourceType Type;
        public int Count;
    }
    
    [Serializable]
    public class PlayerData
    {
        public List<ResourceTypeToCount> resourcesCounts = new List<ResourceTypeToCount>();
    }
}
using System;
using System.Collections.Generic;
using Code.Enums;
using UnityEngine;
using UnityEngine.Serialization;

namespace Code.Player.Configs
{
    [CreateAssetMenu(fileName = "PlayerActionConfig", menuName = "ScriptableObjects/Player/PlayerActionConfig", order = 0)]
    public class PlayerActionConfig : ScriptableObject
    {
        [Serializable]
        public class TypeToIcon
        {
            public Sprite Icon;
            public ResourceType Type;

        }
        
        [SerializeField] private float speed = 5f;
        [SerializeField] private float resourceMineRadius = 2f;
        [SerializeField] private float resourceCollectRadius = 2f;
        [SerializeField] private LayerMask resourcesPointMask;
        [SerializeField] private LayerMask resourcesSpotMask;
        [SerializeField] private float dropResourceSpeed = 2f;
        [SerializeField] private List<TypeToIcon> typeToIcons;

        public List<TypeToIcon> TypeToIcons => typeToIcons;

        public float Speed => speed;

        public float ResourceMineRadius => resourceMineRadius;

        public float ResourceCollectRadius => resourceCollectRadius;

        public LayerMask ResourcesPointMask => resourcesPointMask;

        public LayerMask ResourcesSpotMask => resourcesSpotMask;

        public float DropResourceSpeed => dropResourceSpeed;
    }
}
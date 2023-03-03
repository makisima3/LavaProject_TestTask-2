using System;
using System.Collections.Generic;
using Code.Enums;
using UnityEngine;
using UnityEngine.Serialization;

namespace Code.Player.Configs
{

    [CreateAssetMenu(fileName = "TutorialNodeConfig", menuName = "ScriptableObjects/Tutorial/TutorialNodeConfig", order = 1)]
    public class TutorialNodeConfig : ScriptableObject
    {
        [SerializeField] private DoType doType;
        [SerializeField] private ResourceType resourceType;

        public DoType DoType => doType;

        public ResourceType ResourceType => resourceType;
    }
}
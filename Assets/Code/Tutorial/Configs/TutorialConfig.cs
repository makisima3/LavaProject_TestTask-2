using System;
using System.Collections.Generic;
using Code.Enums;
using UnityEngine;
using UnityEngine.Serialization;

namespace Code.Player.Configs
{
    public enum DoType
    {
        Collect,
        Drop
    }
    
    [CreateAssetMenu(fileName = "TutorialConfig", menuName = "ScriptableObjects/Tutorial/TutorialConfig", order = 0)]
    public class TutorialConfig : ScriptableObject
    {
        [SerializeField] private bool isActive;
        [SerializeField] private float minDistArrow;
        [SerializeField] private List<TutorialNodeConfig> tutorialNodes;

        public List<TutorialNodeConfig> TutorialNodes => tutorialNodes;

        public bool IsActive => isActive;

        public float MINDistArrow => minDistArrow;
    }
}
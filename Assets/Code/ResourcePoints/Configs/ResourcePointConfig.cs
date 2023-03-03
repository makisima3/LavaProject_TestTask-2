using Code.Enums;
using Code.Resources;
using UnityEngine;
using UnityEngine.Serialization;

namespace Code.ResourcePoints.Configs
{
    [CreateAssetMenu(fileName = "ResourcePointConfig", menuName = "ScriptableObjects/Base/ResourcePointConfig", order = 0)]
    public class ResourcePointConfig : ScriptableObject
    {
        [SerializeField] private AnimationType animationType;
        [SerializeField] private ResourceType resourceType;
        [SerializeField] private float reloadTime = 5f;
        [SerializeField] private float attackSpeed = 1f;
        [SerializeField] private float shakeForce = 1f;
        [SerializeField] private float shakeTime = 0.2f;
        [SerializeField] private int hitsToReload = 1;
        [SerializeField] private uint resourcesPerHit = 1;
        [SerializeField] private float colorTime = 0.5f;
        [SerializeField] private Color reloadColor = Color.gray;
        [SerializeField] private Color normalColor = Color.white;

        public AnimationType AnimationType => animationType;

        public float ColorTime => colorTime;

        public float ShakeForce => shakeForce;

        public float ShakeTime => shakeTime;

        public ResourceType ResourceType => resourceType;

        public int HitsToReload => hitsToReload;

        public float ReloadTime => reloadTime;

        public float AttackSpeed => attackSpeed;

        public uint ResourcesPerHit => resourcesPerHit;

        public Color ReloadColor => reloadColor;

        public Color NormalColor => normalColor;
    }
}
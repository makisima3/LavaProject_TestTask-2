using Code.Enums;
using Code.Resources;
using DG.Tweening;
using UnityEngine;

namespace Code.ResourcePoints.Configs
{
    [CreateAssetMenu(fileName = "ResourceSpotConfig", menuName = "ScriptableObjects/Base/ResourceSpotConfig", order = 0)]
    public class ResourceSpotConfig : ScriptableObject
    {
        [SerializeField] private ResourceType collectResourceType;
        [SerializeField] private ResourceType resultResourceType;
        [SerializeField] private float convertTime = 1;
        [SerializeField] private int  resourceCountToConvert = 10;
        [SerializeField] private int  resultRecourseCount = 10;
        [SerializeField] private float spawnTime = 0.2f;
        [SerializeField] private float shakeForce = 1f;
        [SerializeField] private float shakeTime = 0.2f;
        

        public ResourceType CollectResourceType => collectResourceType;

        public ResourceType ResultResourceType => resultResourceType;

        public float ShakeForce => shakeForce;

        public float ShakeTime => shakeTime;

        public float ConvertTime => convertTime;

        public float SpawnTime => spawnTime;

        public int ResultRecourseCount => resultRecourseCount;

        public int ResourceCountToConvert => resourceCountToConvert;

    }
}
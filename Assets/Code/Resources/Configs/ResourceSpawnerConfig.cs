using Code.Enums;
using Code.Resources;
using DG.Tweening;
using UnityEngine;

namespace Code.ResourcePoints.Configs
{
    [CreateAssetMenu(fileName = "ResourceSpawnerConfig", menuName = "ScriptableObjects/Base/ResourceSpawnerConfig", order = 0)]
    public class ResourceSpawnerConfig : ScriptableObject
    {
        [SerializeField] private float maxSpawnRadius = 1f;
        [SerializeField] private float minSpawnRadius = 0.5f;
        [SerializeField] private float impulseForce = 1f;
        [SerializeField] private Resource[] resourcePrefabs;

        public float MAXSpawnRadius => maxSpawnRadius;

        public float MINSpawnRadius => minSpawnRadius;

        public float ImpulseForce => impulseForce;

        public Resource[] ResourcePrefabs => resourcePrefabs;
    }
}
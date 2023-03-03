using System.Linq;
using Code.Enums;
using Code.ResourcePoints.Configs;
using UnityEngine;

namespace Code.Resources
{
    public class ResourceSpawner : MonoBehaviour
    {
        [SerializeField] private ResourceSpawnerConfig config;
        [SerializeField] private Transform resourcesHolder;
        [SerializeField] private Transform spawnPoint;
        [SerializeField] private Transform heightPoint;
        [SerializeField] private bool createdDetailsCanBeCollected;
        
        
        public Resource CreateResource(ResourceType type)
        {
            var resource = Instantiate(config.ResourcePrefabs.First(r => r.Type == type), resourcesHolder);
            resource.Init(spawnPoint.position, GetRandomPoint(),createdDetailsCanBeCollected);

            return resource;
        }

        private Vector3 GetRandomPoint()
        {
            return new Vector3()
            {
                x = Random.Range(-config.MINSpawnRadius, config.MAXSpawnRadius),
                y = heightPoint.localPosition.y,
                z = Random.Range(-config.MINSpawnRadius, config.MAXSpawnRadius),
            };
        }

        #if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            UnityEditor.Handles.color = Color.magenta;
            UnityEditor.Handles.DrawWireDisc(transform.position , transform.up, config.MINSpawnRadius);
            UnityEditor.Handles.DrawWireDisc(transform.position , transform.up, config.MAXSpawnRadius);
        }
        #endif
    }
}
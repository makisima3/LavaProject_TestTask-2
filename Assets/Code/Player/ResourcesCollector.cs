using System;
using System.Collections;
using System.Linq;
using Code.Enums;
using Code.Player.Configs;
using Code.Player.Data;
using Code.ResourcePoints;
using Code.Resources;
using Code.ResourceSpots;
using UnityEngine;

namespace Code.Player
{
    [RequireComponent(typeof(SphereCollider))]
    public class ResourcesCollector : MonoBehaviour
    {
        [SerializeField] private PlayerActionConfig playerActionConfig;
        [SerializeField] private Transform collectPoint;
        [SerializeField] private MovementController movementController;
        [SerializeField] private PlayerDataHolder playerDataHolder;
        [SerializeField] private ResourceSpawner resourceSpawner;

        private SphereCollider _collider;
        private Coroutine _dropCoroutine;
        private Spot _currentSpot;
        private bool _isDropToSpot;
        

        private void Awake()
        {
            _collider = GetComponent<SphereCollider>();
            _collider.radius = playerActionConfig.ResourceCollectRadius;
            
            
        }

        private void Start()
        {
            movementController.OnStartMoving.AddListener(StopDrop);
            movementController.OnStopMove.AddListener(CheckForSpots);
        }

        private void CheckForSpots()
        {
            var colliders = new Collider[10];

            if (Physics.OverlapSphereNonAlloc(transform.position, playerActionConfig.ResourceMineRadius, colliders, playerActionConfig.ResourcesSpotMask) <= 0)
                return;
            
            foreach (var collider in colliders.Where(c => c!= null).OrderBy(c => Vector3.Distance(transform.position,c.transform.position)))
            {
                if (collider.TryGetComponent<Spot>(out var spot))
                {
                    _currentSpot = spot;
                        
                    StartDrop();
                        
                    return;
                }
            }
        }

        private void StartDrop()
        {
            StopDrop();
            _dropCoroutine = StartCoroutine(DropCoroutine());
        }

        private void StopDrop()
        {
            _isDropToSpot = false;
            if (_dropCoroutine == null)
                return;
            
            StopCoroutine(_dropCoroutine);
            _dropCoroutine = null;
            
            playerDataHolder.Save();
           
        }
        
        private void OnResourceCollected(ResourceType type)
        {
            playerDataHolder.AddResource(type, 1);
            playerDataHolder.Save();
        }

        private IEnumerator DropCoroutine()
        {
            _isDropToSpot = true;
            
            //todo: optimize 
            while (playerDataHolder.PlayerData.resourcesCounts.First(r => r.Type == _currentSpot.ResourceType).Count > 0 &&
                   _currentSpot.TryAddResource())
            {
                playerDataHolder.RemoveResource(_currentSpot.ResourceType,1);
                var resource = resourceSpawner.CreateResource(_currentSpot.ResourceType);
                resource.Collect(_currentSpot.InPoint);

                yield return new WaitForSeconds(1 / playerActionConfig.DropResourceSpeed);
            }
            
            playerDataHolder.Save();
        }
        

        private void OnTriggerEnter(Collider other)
        {
            if(_isDropToSpot)
                return;
            
            if (other.TryGetComponent<Resource>(out var resource) && resource.CanBeCollected)
            {
                resource.Collect(collectPoint,() => OnResourceCollected(resource.Type));
            }
        }
        
    }
}
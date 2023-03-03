using System;
using System.Collections.Generic;
using System.Linq;
using Code.Enums;
using Code.Player.Configs;
using Code.Player.Data;
using Code.ResourcePoints;
using Code.ResourceSpots;
using UnityEngine;

namespace Code.Player
{
    public class TutorialManager : MonoBehaviour
    {
        [SerializeField] private PlayerDataHolder playerDataHolder;
        [SerializeField] private TutorialConfig tutorialConfig;
        [SerializeField] private Transform levelHolder;

        private Transform _currentTarget;
        private TutorialNodeConfig _currentNode;
        private List<Transform> _targets;
        private bool _isTutorialFinished;


        public float ArrowMinDist => tutorialConfig.MINDistArrow;
        public Transform CurrentTarget => _currentTarget;

        private void Start()
        {
            if (!tutorialConfig.IsActive)
            {
                _isTutorialFinished = true;
                return;
            }

            UpdateTarget();

            playerDataHolder.OnMoneyResourcesChanged.AddListener(OnResourcesCountCHanged);
        }

        private void OnResourcesCountCHanged(ResourceTypeToCount resourceTypeToCount,bool isAdd)
        {
            if (_isTutorialFinished)
            {
                playerDataHolder.OnMoneyResourcesChanged.RemoveListener(OnResourcesCountCHanged);
                return;
            }

            var isCollect = _currentNode.DoType == DoType.Collect;
            
            if (resourceTypeToCount.Type == _currentNode.ResourceType && 
                isCollect == isAdd)
                UpdateTarget();
        }
        

        private void UpdateTarget()
        {
            SetTutorialNode();

            if (_currentNode == null)
            {
                _isTutorialFinished = true;
                return;
            }

            _targets = new List<Transform>();
            if (_currentNode.DoType == DoType.Collect)
                GetResourcePoints(levelHolder, _targets, _currentNode.ResourceType);
            else
                GetSpots(levelHolder, _targets, _currentNode.ResourceType);
        }

        private void SetTutorialNode()
        {
            if (_currentNode == null)
            {
                _currentNode = tutorialConfig.TutorialNodes.First();
                return;
            }

            var index = tutorialConfig.TutorialNodes.IndexOf(_currentNode) + 1;

            if (index < tutorialConfig.TutorialNodes.Count)
            {
                _currentNode = tutorialConfig.TutorialNodes[index];
                return;
            }

            _currentTarget = null;
            _currentNode = null;
        }


        private void Update()
        {
            if(_isTutorialFinished)
                return;
            
            _currentTarget = _targets.OrderBy(t => Vector3.Distance(t.position, playerDataHolder.transform.position)).FirstOrDefault();
        }

        private void GetResourcePoints(Transform transform, List<Transform> resourcePoints, ResourceType type)
        {
            resourcePoints ??= new List<Transform>();

            if (transform.TryGetComponent<ResourcePoint>(out var resourcePoint) && resourcePoint.Type == type)
            {
                resourcePoints.Add(resourcePoint.transform);
            }

            foreach (Transform child in transform)
            {
                GetResourcePoints(child, resourcePoints, type);
            }
        }

        private void GetSpots(Transform transform, List<Transform> spots, ResourceType type)
        {
            spots ??= new List<Transform>();

            if (transform.TryGetComponent<Spot>(out var spot) && spot.ResourceType == type)
            {
                spots.Add(spot.transform);
            }

            foreach (Transform child in transform)
            {
                GetSpots(child, spots, type);
            }
        }

    }
}
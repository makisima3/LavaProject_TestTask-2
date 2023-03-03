using System;
using System.Collections;
using Code.Enums;
using Code.ResourcePoints.Configs;
using Code.Resources;
using Code.UI;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Code.ResourceSpots
{
    public class Spot : MonoBehaviour
    {
        [SerializeField] private SpotView spotView;
        [SerializeField] private ResourceSpotConfig config;
        [SerializeField] private ResourceSpawner resourceSpawner;
        [SerializeField] private Transform inPoint;
        [SerializeField] private Transform view;

        private int _resourceCount;
        private bool _isConverting;

        public Transform InPoint => inPoint;

        public ResourceType ResourceType => config.CollectResourceType;

        private void Awake()
        {
            spotView.Init(ResourceType, config.ResultResourceType,config.ResourceCountToConvert,config.ResultRecourseCount);
        }

        public bool TryAddResource()
        {
            if (_resourceCount + 1 > config.ResourceCountToConvert || _isConverting)
                return false;
            
            AddResource();
            
            return true;
        }

        private void AddResource()
        {
            _resourceCount++;

            if (_resourceCount == config.ResourceCountToConvert)
                StartCoroutine(Convert());
            
            spotView.SetCount(config.ResourceCountToConvert - _resourceCount);

        }
        

        private IEnumerator Convert()
        {
            _isConverting = true;

            yield return new WaitForSeconds(config.ConvertTime);

            for (int i = 0; i < config.ResultRecourseCount; i++)
            {
                resourceSpawner.CreateResource(config.ResultResourceType);
                Animate();
                yield return new WaitForSeconds(config.SpawnTime);
            }

            _resourceCount = 0;
            spotView.SetCount(config.ResourceCountToConvert);
            _isConverting = false;

        }
        
        private void Animate()
        {
            view.DOShakeScale(config.ShakeTime,config.ShakeForce);
        }
    }
}
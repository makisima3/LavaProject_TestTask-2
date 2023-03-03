using System;
using System.Collections;
using System.Collections.Generic;
using Code.Enums;
using Code.ResourcePoints.Configs;
using Code.Resources;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Code.ResourcePoints
{
    [RequireComponent(typeof(AudioSource))]
    public class ResourcePoint : MonoBehaviour
    {
        [SerializeField] private ResourcePointConfig config;
        [SerializeField] private ResourceSpawner resourceSpawner;
        [SerializeField] private Transform view;
        [SerializeField] private AudioClip[] hitClips;
        [SerializeField] private MeshRenderer[] meshRenderers;


        private int _hitCount;
        private bool _isReload;
        private AudioSource _audioSource;

        public AnimationType AnimationType => config.AnimationType;
        public ResourceType Type => config.ResourceType;
        public float AttackSpeed => config.AttackSpeed;
        public bool CanBeHit => !_isReload;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
            
            foreach (var meshRenderer in meshRenderers)
            {
                meshRenderer.sharedMaterial = new Material(meshRenderer.material);
                meshRenderer.sharedMaterial.color = config.NormalColor;
            }
        }

        public void Hit()
        {
            if (_isReload)
                return;

            _audioSource.PlayOneShot(hitClips[Random.Range(0, hitClips.Length)]);
            _hitCount++;

            if (_hitCount >= config.HitsToReload)
            {
                StartCoroutine(Reload());
            }

            for (int i = 0; i < config.ResourcesPerHit; i++)
            {
                resourceSpawner.CreateResource(config.ResourceType);
            }
            AnimateHit();
        }

        private IEnumerator Reload()
        {
            _isReload = true;
            AnimateReload(_isReload);
            yield return new WaitForSeconds(config.ReloadTime);

            _hitCount = 0;
            _isReload = false;
            AnimateReload(_isReload);
        }

        private void AnimateHit()
        {
            view.DOShakeScale(config.ShakeTime, config.ShakeForce);
        }
        
        private void AnimateReload(bool isReload)
        {
            foreach (var meshRenderer in meshRenderers)
            {
                meshRenderer.sharedMaterial.DOColor(isReload ? config.ReloadColor : config.NormalColor,config.ColorTime);
            }
        }
    }
}
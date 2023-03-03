using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Code.Sound
{
    [RequireComponent(typeof(AudioSource))]
    public class SoundManager : MonoBehaviour
    {
        [SerializeField] private List<AudioClip> clips;

        private AudioSource _audioSource;
        private int _index;
        private void Awake()
        {
            _index = Random.Range(0,clips.Count);
            _audioSource = GetComponent<AudioSource>();
            PlayNextClip();
        }

        private void PlayNextClip()
        {
            _index = Random.Range(0,clips.Count);
            _audioSource.clip = clips[_index];
            _audioSource.Play();

            StartCoroutine(Delay());
        }
        

        private IEnumerator Delay()
        {
            yield return new WaitForSeconds(clips[_index].length);

            PlayNextClip();
        }
    }
}
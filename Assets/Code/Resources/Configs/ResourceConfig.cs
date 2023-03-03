using Code.Enums;
using DG.Tweening;
using UnityEngine;

namespace Code.ResourcePoints.Configs
{
    [CreateAssetMenu(fileName = "ResourceConfig", menuName = "ScriptableObjects/Base/ResourceConfig", order = 0)]
    public class ResourceConfig : ScriptableObject
    {
        [SerializeField] private ResourceType resourceType;
        [SerializeField] private float jumpForce = 1f;
        [SerializeField] private Ease collectEase;
        [SerializeField] private float dropTime = 1f;
        [SerializeField] private float collectTime=  1f;
        [SerializeField] private float collectDelay = 1f;
        

        public ResourceType ResourceType => resourceType;

        public Ease CollectEase => collectEase;

        public float JumpForce => jumpForce;

        public float CollectTime => collectTime;

        public float DropTime => dropTime;

        public float CollectDelay => collectDelay;
    }
}
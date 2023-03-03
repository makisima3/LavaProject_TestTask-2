using System;
using System.Collections;
using System.Linq;
using Code.Enums;
using Code.Player.Data;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UI
{
    public class ResourcesCountView : MonoBehaviour
    {
        [SerializeField] private ResourceView resourceViewPrefab;
        [SerializeField] private Transform holder;
        [SerializeField] private PlayerDataHolder playerDataHolder;
        [SerializeField] private HorizontalLayoutGroup group;

        private void Start()
        {
            foreach (ResourceType resource in Enum.GetValues(typeof(ResourceType)))
            {
                var resourceView = Instantiate(resourceViewPrefab, holder);
                var resourcesCount = playerDataHolder.PlayerData.resourcesCounts.FirstOrDefault(t => t.Type == resource);
                resourceView.Init(playerDataHolder, resource, resourcesCount?.Count ?? 0);
            }

            StartCoroutine(UnityFix());
        }

        private IEnumerator UnityFix()
        {
            while (true)
            {
                group.spacing = 1;
                yield return new WaitForEndOfFrame();
                group.spacing = 0;

            }

        }
    }
}
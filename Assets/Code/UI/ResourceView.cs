using System;
using System.Collections.Generic;
using System.Linq;
using Code.Enums;
using Code.Player.Configs;
using Code.Player.Data;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UI
{
    public class ResourceView : MonoBehaviour
    {
        [SerializeField] private TMP_Text countPlace;
        [SerializeField] private Image icon;
        [SerializeField] private PlayerActionConfig playerActionConfig;
       

        private ResourceType _type;
        private int _currentCount;
        private Tween _countTween;
        
        public void Init(PlayerDataHolder playerDataHolder,ResourceType type, int count)
        {
            _type = type;
            countPlace.text = count.ToString();
            icon.sprite = playerActionConfig.TypeToIcons.First(t => t.Type == _type).Icon;
            _currentCount = count;
            
            playerDataHolder.OnMoneyResourcesChanged.AddListener(OnResourcesChanged);

            if (_currentCount <= 0)
                gameObject.SetActive(false);
        }

        private void OnResourcesChanged(ResourceTypeToCount typeToCount,bool isAdd)
        {
            if(typeToCount.Type != _type)
                return;
            
            if(isAdd)
                SetToUpper(typeToCount.Count);
            else
                SetToLow(typeToCount.Count);
        }

        private void SetToLow(int count)
        {
            _currentCount = count;
            countPlace.text = _currentCount.ToString();
            
            gameObject.SetActive(_currentCount > 0);
        }
        
        private void SetToUpper(int count)
        {
            //todo:moveToconfigs
            _countTween.Kill();
            _countTween = DOTween.To(Getter, Setter, count, 1f).SetEase(Ease.Linear);
            
            gameObject.SetActive(count > 0);;
        }

        private int Getter() => _currentCount;

        private void Setter(int count)
        {
            _currentCount = count;
            countPlace.text = _currentCount.ToString();
        }
    }
}
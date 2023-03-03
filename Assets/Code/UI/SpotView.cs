using System;
using System.Linq;
using Code.Enums;
using Code.Player.Configs;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UI
{
    public class SpotView : MonoBehaviour
    {
        [SerializeField] private TMP_Text countFrom;
        [SerializeField] private TMP_Text countTo;
        [SerializeField] private Image iconFrom;
        [SerializeField] private Image iconTo;
        [SerializeField] private PlayerActionConfig playerActionConfig;

        private int remainResourceCount;
        
        public void Init(ResourceType iconFrom, ResourceType iconTo, int countFrom,int countTo)
        {
            this.iconFrom.sprite = playerActionConfig.TypeToIcons.First(t => t.Type == iconFrom).Icon;
            this.iconTo.sprite = playerActionConfig.TypeToIcons.First(t => t.Type == iconTo).Icon;
            this.countTo.text = countTo.ToString();
            this.countFrom.text = countFrom.ToString();
        }

        public void SetCount(int remain)
        {
            countFrom.text = remain.ToString();
        }
    }
}
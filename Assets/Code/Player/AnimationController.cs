using System;
using System.Collections.Generic;
using System.Linq;
using Code.Enums;
using UnityEngine;

namespace Code.Player
{
    [RequireComponent(typeof(Animator))]
    public class AnimationController : MonoBehaviour
    {
        [Serializable]
        public class TypeToAnimation
        {
            [SerializeField] private string name;
            [SerializeField] private AnimationType type;
            [SerializeField] private GameObject tool;

            public string Name => name;

            public AnimationType Type => type;

            public GameObject Tool => tool;
        }
        
        [SerializeField] private List<TypeToAnimation> typeToAnimations;
        

        private Animator animator;

        private void Awake()
        {
            animator = GetComponent<Animator>();
        }
        
        public void SetBool(AnimationType type,bool value)
        {
            var typeToAnimation = typeToAnimations.FirstOrDefault(t => t.Type == type);

            if (typeToAnimation == null)
                throw new Exception("Didn't find the animation");
            
            animator.SetBool(typeToAnimation.Name, value);

            ActivateTool(typeToAnimation);
        }
        
        public void SetStrigger(AnimationType type)
        {
            var typeToAnimation = typeToAnimations.FirstOrDefault(t => t.Type == type);

            if (typeToAnimation == null)
                throw new Exception("Didn't find the animation");
            
            animator.SetTrigger(typeToAnimation.Name);

            ActivateTool(typeToAnimation);
        }

        private void ActivateTool(TypeToAnimation currentTypeToAnimation)
        {
            foreach (var typeToAnimation in typeToAnimations.Where(typeToAnimation => typeToAnimation.Tool != null))
            {
                typeToAnimation.Tool.SetActive(false);
            }

            if (currentTypeToAnimation.Tool != null)
                currentTypeToAnimation.Tool.SetActive(true);
        }
        
    }
}
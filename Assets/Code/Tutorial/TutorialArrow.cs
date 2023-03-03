using System;
using UnityEngine;
using UnityEngine.Events;

namespace Code.Player
{
    public class TutorialArrow : MonoBehaviour
    {
        [SerializeField] private Transform view;
        [SerializeField] private MovementController movementController;
        [SerializeField] private TutorialManager tutorialManager;
        
       private float minDist = 1f;
       private Transform _target => tutorialManager.CurrentTarget;

       private void Awake()
       {
           minDist = tutorialManager.ArrowMinDist;
       }

        private void LateUpdate()
        {
            if (_target == null)
            {
                view.gameObject.SetActive(false);
                return;
            }

            transform.position = movementController.transform.position;
            view.rotation = GetRotation(transform.position, _target.transform.position, Vector3.up);

            view.gameObject.SetActive(!(Vector3.Distance(transform.position, _target.transform.position) <= minDist));
        }
        
        private Quaternion GetRotation(Vector3 from, Vector3 to, Vector3 axis)
        {
            //TODO: Make method universal for any direction and axis
            var direction = (to - from).normalized;
            var angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            
            return Quaternion.Euler(axis * angle);
          
        }
    }
}
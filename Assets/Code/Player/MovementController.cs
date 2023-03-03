using System;
using Code.Enums;
using Code.Player.Configs;
using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

namespace Code.Player
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class MovementController : MonoBehaviour
    {
        [SerializeField] private Joystick joystick;
        [SerializeField] private PlayerActionConfig playerActionConfig;
        [SerializeField] private AnimationController animationController;
        
        private NavMeshAgent _navMeshAgent;
        private bool _isMoving;
        private Vector3 _moveDirection;
        
        public UnityEvent OnStartMoving { get; private set; }
        public UnityEvent OnStopMove { get; private set; }
        public bool IsMoving => _isMoving;

        private void Awake()
        {
            OnStartMoving = new UnityEvent();
            OnStopMove = new UnityEvent();
            
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _navMeshAgent.speed = playerActionConfig.Speed; 
            
            OnStartMoving.AddListener(() => animationController.SetBool(AnimationType.Move,true));
            OnStopMove.AddListener(() => animationController.SetBool(AnimationType.Move,false));
        }

        private void Update()
        {
            _moveDirection = new Vector3()
            {
                x = joystick.Direction.x,
                z = joystick.Direction.y,
            };

            _navMeshAgent.SetDestination(transform.position + _moveDirection);
            if (_moveDirection != Vector3.zero && !_isMoving)
            {
                OnStartMoving.Invoke();
                Debug.Log("Start Move");
            }
            if (_moveDirection == Vector3.zero && _isMoving)
            {
                OnStopMove.Invoke();
                Debug.Log("Stop Move");
            }

            _isMoving = _moveDirection != Vector3.zero;
        }

        public void RotateTo(Transform point)
        {
            transform.DORotateQuaternion(GetRotation(transform.position, point.position, Vector3.up),1f);
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
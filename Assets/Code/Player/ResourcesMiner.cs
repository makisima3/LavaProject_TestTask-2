using System;
using System.Collections;
using System.Linq;
using Code.Enums;
using Code.Player.Configs;
using Code.ResourcePoints;
using UnityEngine;

namespace Code.Player
{
    [RequireComponent(typeof(MovementController))]
    public class ResourcesMiner : MonoBehaviour
    {
        [SerializeField] private PlayerActionConfig playerActionConfig;
        [SerializeField] private AnimationController animationController;
        
        private MovementController _movementController;
        private Coroutine _attackCoroutine;
        private AnimationType _currentAnimationType;
        private ResourcePoint _currentResourcePoint;
        private bool _isAttacking;
        
        private void Start()
        {
            _movementController = GetComponent<MovementController>();
            _movementController.OnStopMove.AddListener(CheckForResourcesPoint);
            _movementController.OnStartMoving.AddListener(StopAttack);
        }

        private void CheckForResourcesPoint()
        {
            var colliders = new Collider[10];

            if (Physics.OverlapSphereNonAlloc(transform.position, playerActionConfig.ResourceMineRadius, colliders, playerActionConfig.ResourcesPointMask) <= 0)
                return;
            
            foreach (var collider in colliders.Where(c => c != null).OrderBy(c => Vector3.Distance(transform.position,c.transform.position)))
            {

                if (collider.TryGetComponent<ResourcePoint>(out var resourcePoint))
                {
                    _currentResourcePoint = resourcePoint;
                    _currentAnimationType = _currentResourcePoint.AnimationType;
                        
                    StartAttack();
                        
                    return;
                }
            }
        }

        private void StartAttack()
        {
            StopAttack();

            _movementController.RotateTo(_currentResourcePoint.transform);
            _isAttacking = true;
            _attackCoroutine = StartCoroutine(AttackCoroutine());
        }
        
        private void StopAttack()
        {
            if(!_isAttacking)
                return;
            
            _isAttacking = false;
           
            
            if (_attackCoroutine == null)
                return;
            
            StopCoroutine(_attackCoroutine);
            _attackCoroutine = null;
        }

        private IEnumerator AttackCoroutine()
        {
            while (true)
            {
                if (!_currentResourcePoint.CanBeHit)
                {
                    yield return new WaitForEndOfFrame();
                    continue;
                }
                
               //todo: сделать анимацию атаки зависимым от скорости атаки
                animationController.SetStrigger(_currentAnimationType);
                yield return new WaitForSeconds(_currentResourcePoint.AttackSpeed);
                _currentResourcePoint.Hit();
            }
        }
    }
}
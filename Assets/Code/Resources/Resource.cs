using System;
using System.Collections;
using Code.Enums;
using Code.ResourcePoints.Configs;
using DG.Tweening;
using DG.Tweening.Core;
using UnityEngine;

namespace Code.Resources
{
    public class Resource : MonoBehaviour
    {
        [SerializeField] private ResourceConfig config;

        public ResourceType Type => config.ResourceType;
        private bool _isCollected;
        private Tween _jumpTween;
        private Tweener _moveTween;
        private bool _isMove;
        private bool _canBeCollected;
        private Coroutine _moveEndAwaiter;

        public bool CanBeCollected => _canBeCollected;

        public void Init(Vector3 startPosition, Vector3 endPoint, bool canBeCollected)
        {
            _canBeCollected = canBeCollected;
            transform.position = startPosition;
            _isMove = true;
            //in accordance with the technical task, there should be physics here, but I thought it would be more correct to use DOTween
            _jumpTween = transform.DOLocalJump(endPoint, 10, 1, config.DropTime).OnComplete(() => _isMove = false);
        }

        public void Collect(Transform point, Action onMoveEnd = null)
        {
            if (_isCollected)
                return;

            if (_moveEndAwaiter != null)
            {
                StopCoroutine(_moveEndAwaiter);
                _moveEndAwaiter = null;
            }
            _moveEndAwaiter = StartCoroutine(MoveEndAwaiter(point, onMoveEnd));
        }

        private IEnumerator MoveEndAwaiter(Transform point, Action onMoveEnd = null)
        {
            yield return new WaitWhile(() => _isMove);
            var distance = Vector3.Distance(transform.position, point.position);
            _jumpTween.Kill();
            _moveTween.Kill();
            
            //Trash
            //думаю тут лучше на транформе сделать, а дотвин нафиг
            _moveTween = transform
                .DOMove(point.position, GetTime(distance, config.JumpForce))
                .SetEase(config.CollectEase)
                .SetDelay(config.CollectDelay)
                .OnUpdate(() =>
                {
                    distance = Vector3.Distance(transform.position, point.position);
                    var time = GetTime(distance, config.JumpForce * distance);
                    if (distance > 1f)
                    {
                        _moveTween.ChangeEndValue(point.position,time ,true);
                    }
                })
                .OnComplete(() =>
                {
                    onMoveEnd?.Invoke();
                    Destroy(gameObject);
                });
            _isCollected = true;
        }
        private float GetTime(float distance, float speed)
        {
            return distance / speed;
        }

    }
}
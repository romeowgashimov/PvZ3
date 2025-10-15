using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using static Cysharp.Threading.Tasks.UniTask;
using static UnityEngine.Object;
using static UnityEngine.Resources;

namespace _Project.Logic.Core
{
    public class CardAnimator
    {
        private Transform _currentCardsView;
        private Transform _nextTransform;
        private float _duration;
        private Vector3 _nextPosition;

        public CardAnimator(Transform currentCardsView, float  duration = 1f)
        {
            _currentCardsView = currentCardsView;
            _duration = duration;
        }

        public void InitializeNextPosition()
        {
            _nextTransform = Load<Transform>($"nextPosition");
            _nextTransform = Instantiate(_nextTransform, _currentCardsView, true);
        }

        public UniTask Animate(Transform card, Transform cardView = null)
        {
            if (cardView != null)
            {
                card.SetParent(card.transform.parent.parent.parent);
                return WaitForSeconds(card.DOMove(cardView.position, _duration).Duration());
            }

            card.SetParent(card.transform.parent.parent.parent.parent);
            _nextPosition = _nextTransform.position;
            _nextTransform.SetParent(null);
            
            return WaitForSeconds(card.DOMove(_nextPosition, _duration).Duration());
        }

        public void Reset() => 
            _nextTransform.SetParent(_currentCardsView);
    }
}
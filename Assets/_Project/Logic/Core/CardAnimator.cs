using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using static Cysharp.Threading.Tasks.UniTask;

namespace _Project.Logic.Core
{
    public class CardAnimator
    {
        private Dictionary<Transform, bool> _nextTransform;
        private float _duration;
        private Vector3 _nextPosition;

        public CardAnimator(Dictionary<Transform, bool> nextTransform, float duration = .4f)
        {
            _nextTransform = nextTransform;
            _duration = duration;
        }

        public UniTask Animate(Transform card, Transform cardView = null)
        {
            if (cardView != null)
            {
                _nextTransform[card.transform.parent] = false;
                
                card.SetParent(card.transform.root);
                return WaitForSeconds(card.DOMove(cardView.position, _duration).Duration());
            }
            
            KeyValuePair<Transform, bool> keyValuePair = _nextTransform.First(x => !x.Value);
            _nextTransform[keyValuePair.Key] = true;
            _nextPosition = keyValuePair.Key.position;
            
            card.SetParent(card.transform.root);
            return WaitForSeconds(card.DOMove(_nextPosition, _duration).Duration());
        }
    }
}
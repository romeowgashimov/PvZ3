using UnityEngine;
using UnityEngine.EventSystems;

namespace _Project.Logic.Core
{
    internal class CardAnimator
    {
        private Vector3 _initialPosition;
        private RectTransform _cardTransform;
        private float _scaler;
        
        public void OnBeginDrag(bool beginDrag, RectTransform cardTransform)
        {
            _initialPosition = cardTransform.position;
            _cardTransform = cardTransform;
        }

        public void OnDrag(Vector2 delta) => 
            _cardTransform.anchoredPosition += delta / _scaler;

        public void OnEndDrag(bool endDrag)
        {
            _cardTransform.position = _initialPosition;
        }
        
        public void Setup(float scaler) =>
            _scaler = scaler;
    }
}
using _Project.Logic.Core;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using static Cysharp.Threading.Tasks.UniTask;
using static UnityEngine.Screen;

namespace _Project.Logic.Implementation.WindowAnimators
{
    public class SlideUpWindowAnimator : WindowAnimator
    {
        [SerializeField] private RectTransform _rectTransform;
        [SerializeField] private float _duration = 1f;
        [SerializeField, Range(-10, 10)] private float _endValueForShow = 4f;
        [SerializeField, Range(-10, 10)] private float _endValueForHide = -4f;

        public override void Prepare() => 
            _rectTransform.anchoredPosition = -Vector2.up * height;

        public override UniTask Show()
        {
            gameObject.SetActive(true);
            return WaitForSeconds(_rectTransform.DOMoveY(_endValueForShow, _duration).Duration());
        }

        public override UniTask Hide()
        {
            gameObject.SetActive(false);
            return WaitForSeconds(_rectTransform.DOMoveY(_endValueForHide, _duration).Duration());
        }
    }
}
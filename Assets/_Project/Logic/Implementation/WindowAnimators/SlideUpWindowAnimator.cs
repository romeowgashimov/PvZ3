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
        [SerializeField, Range(-10, 10)] private float _endValueForShow = 4f;
        [SerializeField, Range(-10, 10)] private float _endValueForHide = -4f;

        public override void Prepare() => 
            _rectTransform.anchoredPosition = -Vector2.up * height;

        public override UniTask Show()
        {
            gameObject.SetActive(true);
            return WaitForSeconds(_rectTransform.DOMoveY(_endValueForShow, Duration).Duration());
        }

        public override async UniTask Hide()
        {
            await WaitForSeconds(_rectTransform.DOMoveY(_endValueForHide, Duration).Duration());
            gameObject.SetActive(false);
        }
    }
}
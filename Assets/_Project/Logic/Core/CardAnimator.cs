using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using static Cysharp.Threading.Tasks.UniTask;

namespace _Project.Logic.Core
{
    public class CardAnimator
    {
        private Dictionary<Transform, bool> _currentPlantsViewPos;
        private Dictionary<string, Transform> _cardsInCurrentPlantsView;
        private Dictionary<string, Transform> _cardsInChoicePlantsView;
        private float _duration;

        public CardAnimator(Dictionary<Transform, bool> currentPlantsViewPos,
                            Dictionary<string, Transform> cardsInChoicePlantsView,
                            float duration = .4f)
        {
            _currentPlantsViewPos = currentPlantsViewPos;
            _cardsInChoicePlantsView = cardsInChoicePlantsView;
            _cardsInCurrentPlantsView = new();
            _duration = duration;
        }

        public async void Animate(Card card)
        {
            if (_cardsInCurrentPlantsView.Remove(card.PlantId, out Transform currentPosition))
            {
                _currentPlantsViewPos[currentPosition] = false;
                currentPosition = _cardsInChoicePlantsView[card.PlantId];
            }
            else
            {
                currentPosition = _currentPlantsViewPos.First(x => !x.Value).Key;
                _currentPlantsViewPos[currentPosition] = true;
                _cardsInCurrentPlantsView.Add(card.PlantId, currentPosition);
            }
            
            card.transform.SetParent(card.transform.root);
            await WaitForSeconds(card.transform.DOMove(currentPosition.position, _duration).Duration());
            
            card.transform.SetParent(currentPosition);
            card.transform.localPosition = Vector3.zero;
        }
    }
}
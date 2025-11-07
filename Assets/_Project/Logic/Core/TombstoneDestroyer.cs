using static _Project.Logic.Core.SlotType;
using static UnityEngine.Mathf;
using static UnityEngine.Time;

namespace _Project.Logic.Core
{
    public class TombstoneDestroyer : Plant
    {
        private float _timeToDestroy = 6f;      

        protected override void Prepare() => 
            _slotType = WithOther;

        public override void Run()
        {
            _timeToDestroy -= Max(_timeToDestroy - fixedDeltaTime, 0);

            if (_timeToDestroy <= 0f)
                Death(Line);
        }
        
        public override void Boost()
        {
            
        }
    }
}
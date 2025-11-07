using System;
using UnityEngine;
using Zenject;
using static _Project.Logic.Core.SlotType;

namespace _Project.Logic.Core
{
    [RequireComponent(typeof(CanvasGroup))]
    public abstract class Plant : MonoBehaviour, IUseable
    {
        [field: SerializeField] protected float _cooldownTime;
        [Inject] public Canvas Canvas { get; private set; }

        protected SlotType _slotType = Free;

        public bool IsCooldown => CooldownDeltaTime > 0;
        public float CooldownDeltaTime { get; protected set; }
        public Line Line { get; private set; }
        public Zombie Enemy { get; private set; }
        public bool HasEnemy => Enemy != null;
        public Vector3 Position => transform.position;
        public CanvasGroup CanvasGroup { get; private set; }
        public RectTransform RectTransform { get; private set; }

        public event Action<Line> OnPlaced;
        public event Action<Line> OnDead;
        
        protected void Awake()
        {
            RectTransform = GetComponent<RectTransform>();
            CanvasGroup = GetComponent<CanvasGroup>();

            Prepare();
        }

        public void Use(Slot slot)
        {
            if (slot.SlotType != _slotType)
                return;
                
            slot.ChangeCurrentPlant(this, WithPlant);
            Line = slot.Line;
            
            OnDead += Release;

            void Release(Line _)
            {
                OnDead -= Release;
                slot.ChangeCurrentPlant(null, Free);
            }
            
            transform.SetParent(slot.transform, false);
            transform.localPosition = Vector3.zero;
            
            OnPlaced?.Invoke(slot.Line);
        }

        public void Death(Line line) => 
            OnDead?.Invoke(line);

        public void SetEnemy(Zombie zombie)
        {
            if (zombie == null || zombie.Position.x - Position.x > 0)
                Enemy = zombie;
        }

        protected virtual void Prepare() { }
        
        public abstract void Run();

        public abstract void Boost();
    }
}
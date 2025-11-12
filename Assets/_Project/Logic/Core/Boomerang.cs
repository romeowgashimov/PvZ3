using UnityEngine;
using Zenject;
using static UnityEngine.Time;

namespace _Project.Logic.Core
{
    public class Boomerang : Plant, IDamageable
    {
        [SerializeField] private BoomerangBullet _bulletPrefab;
        [Inject] private IInstantiator _instantiator;
        [Inject] private RunableRepository _runableRepository;

        private BulletSystem _bulletSystem;
        
        [field: SerializeField] public int Damage { get; private set; }
        
        protected override void Prepare()
        {
            _bulletSystem = new(_bulletPrefab, _instantiator, this, _runableRepository);
            Recharge();
        }

        public override void Run()
        {
            if (HasEnemy && !IsCooldown)
            {
                _bulletSystem.Shoot();
                CooldownDeltaTime = _cooldownTime;
            }
        }

        public override void Boost()
        {
        
        }
        
        public void Recharge() => 
            CooldownDeltaTime = 0;
    }
}
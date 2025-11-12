using UnityEngine;
using Zenject;
using static UnityEngine.Mathf;
using static UnityEngine.Time;

namespace _Project.Logic.Core
{
    internal class PeaBullet : Bullet
    {
        [Inject] private ZombieRepository _zombieRepository;
        
        public override void Prepare(Plant plant)
        {
            _damage = ((IDamageable)plant).Damage;
            _line = plant.Line;
        }

        public override void Run()
        {
            transform.position += Vector3.right * (fixedDeltaTime * _speed);

            if (_zombieRepository.TryFindClosestZombie(_line, Position, out Zombie closest))
                if (Abs((closest.Position - Position).x) < _size)
                {
                    closest.GetDamage(_damage);
                    CallTriggerEvent();
                }
        }
    }
}
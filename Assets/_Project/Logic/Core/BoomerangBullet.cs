using UnityEngine;
using Zenject;
using static UnityEngine.Mathf;
using static UnityEngine.Time;

namespace _Project.Logic.Core
{
    internal class BoomerangBullet : Bullet
    {
        //Если растение поставить на третью клетку, то снаряд улетит за экран
        [SerializeField] private float _backDistance = 15.5f;
        [SerializeField] private float _backSpeed = 0.1f;
        [SerializeField] private float _tempDistance;
        [Inject] private ZombieRepository _zombieRepository;
        private Boomerang _parent;
        private Zombie _closest;

        public override void Prepare(Plant plant)
        {
            //Лучше хранить объект в виде интерфейса, тогда не придётся постоянно вызывать Prepare в создании пули
            _damage = ((IDamageable)plant).Damage;
            _parent = (Boomerang)plant;
            _line = plant.Line;
            _tempDistance = _backDistance;
        }

        public override void Run()
        {
            transform.position += Vector3.right * (_tempDistance * _tempDistance * _tempDistance * (fixedDeltaTime * _speed) 
                                                    / (_tempDistance * _tempDistance));
            _tempDistance -= _backSpeed;

            if (_zombieRepository.TryFindClosestZombie(_line, Position, out Zombie closest))
            {
                float distance = Abs((closest.Position - Position).x);
                if (distance < _size && _closest != closest)
                {
                    closest.GetDamage(_damage);
                    _closest = closest;
                }
                
                else if (distance > _size)
                    _closest = null;
            }
            
            if ((transform.position - _parent.Position).x <= 0 && _tempDistance <= 0)
            {
                CallTriggerEvent();
                _parent.Recharge();
            }
        }
    }
}
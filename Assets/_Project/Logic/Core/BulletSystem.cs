using UnityEngine;
using UnityEngine.Pool;
using Zenject;
using static UnityEngine.Object;

namespace _Project.Logic.Core
{
    internal class BulletSystem
    {
        private Bullet _bulletPrefab;
        private Transform _bulletParent;
        private IInstantiator _instantiator;
        private ObjectPool<Bullet> _bulletPool;
        private RunableRepository _runableRepository;
        private Plant _plant;
        
        public BulletSystem(Bullet bulletPrefab, IInstantiator instantiator, Plant plant, RunableRepository runableRepository)
        {
            _bulletPrefab = bulletPrefab;
            _instantiator = instantiator;
            _bulletParent = plant.transform;
            _plant = plant;
            _runableRepository = runableRepository;

            _bulletPool = new(CreateBullet, GetBullet, ReleaseBullet, DestroyBullet);
        }
        
        public void Shoot() =>
            _bulletPool.Get();
        
        private Bullet CreateBullet()
        {
            Bullet bullet = _instantiator.InstantiatePrefabForComponent<Bullet>(_bulletPrefab, _bulletParent.root);
            return bullet; 
        }
        
        private void GetBullet(Bullet bullet)
        {
            bullet.transform.position = _bulletParent.position;
            bullet.gameObject.SetActive(true);
            bullet.enabled = true;
            
            _runableRepository.Register(bullet);
            bullet.Prepare(_plant);
            
            bullet.OnTriggerEnter += Unregister;
            void Unregister()
            {
                bullet.OnTriggerEnter -= Unregister;
                _runableRepository.Unregister(bullet);
                bullet.gameObject.SetActive(false);
                
                _bulletPool.Release(bullet);
            }
        }
        
        private void ReleaseBullet(Bullet bullet) => 
            bullet.enabled = false;

        private void DestroyBullet(Bullet bullet) => 
            Destroy(bullet.gameObject);
    }
}
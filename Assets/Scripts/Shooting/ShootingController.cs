using UnityEngine;

namespace MyGame.Shooting
{
    public class ShootingController : MonoBehaviour
    {
        public bool HasTarget => _target != null;
        public Vector3 TargetPosition => _target.transform.position;
        
        private Weapon _weapon;

        private Collider[] _colliders = new Collider[2];
        // Размер массива - [2], т.к. одним из объектов можем быть мы (игрок) или сам враг
        // Если враг будет искать нас по маске, где ему надо найти только врагов (EnemyMask) - в итоге он увидит себя, а нас не увидит
        private float _nextShotTimerSec;
        private GameObject _target;

        protected void Update()
        {
            _target = GetTarget();

            _nextShotTimerSec -= Time.deltaTime;
            if (_nextShotTimerSec < 0 )
            {
                if (HasTarget)
                    _weapon.Shoot(TargetPosition);
                /* Было:
                var target = transform.forward * 100f;
                _weapon.Shoot(target);
                */

                _nextShotTimerSec = _weapon.ShootFrequencySec;
            }
        }

        public void SetWeapon(Weapon weaponPrefab, Transform hand)
        {
            if (_weapon != null)
                Destroy(_weapon.gameObject);

            _weapon = Instantiate(weaponPrefab, hand);
            _weapon.transform.localPosition = Vector3.zero;
            _weapon.transform.localRotation = Quaternion.identity;
        }

        private GameObject GetTarget() 
        {
            GameObject target = null;

            var position = _weapon.transform.position;
            var radius = _weapon.ShootRadius;
            var mask = LayerUtils.AimMask;

            var size = Physics.OverlapSphereNonAlloc(position, radius, _colliders, mask);    // Данный метод возвращает количество найденных коллайдеров. Для [2]: мб 0, 1 или 2
            if (size > 1 )
            {
                for (int i = 0; i < size; i++)
                {
                    if (_colliders[i].gameObject !=  gameObject)    // Чтобы игрок не стрелял сам в себя (и враг тоже)
                    {
                        target = _colliders[i].gameObject;
                        break;
                    }
                }
            }
            return target;

        }
    }
}
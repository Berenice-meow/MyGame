using MyGame.Movement;
using MyGame.PickUp;
using MyGame.Shooting;
using UnityEngine;

namespace MyGame
{
    [RequireComponent(typeof(CharacterMovementController), typeof(ShootingController))]
    public abstract class BaseCharacter : MonoBehaviour
    {
        [SerializeField] private Weapon _baseWeaponPrefab;

        [SerializeField] private Transform _hand;

        [SerializeField] private float _maxHp = 50f;

        [SerializeField] private float _lowHpCoefficient = 30f;

        private float _currentHp;
        private float _lowHp;
        public bool IsHpLow = false;

        private IMovementDirectionSource _movementDirectionSource;
        private CharacterMovementController _characterMovementController;
        private ShootingController _shootingController;
        private Weapon _currentWeapon;

        protected void Awake()
        {
            _movementDirectionSource = GetComponent<IMovementDirectionSource>();

            _characterMovementController = GetComponent<CharacterMovementController>();
            _shootingController = GetComponent<ShootingController>();

            _currentHp = _maxHp;
            _lowHp = _maxHp * _lowHpCoefficient / 100;

            _currentWeapon = _baseWeaponPrefab;
        }

        protected void Start()
        {
            SetWeapon(_currentWeapon); 
        }

        protected void Update()
        {
            var direction = _movementDirectionSource.MovementDirection;
            var lookDirection = direction;
            if (_shootingController.HasTarget)
                lookDirection = (_shootingController.TargetPosition - transform.position).normalized;

            _characterMovementController.MovementDirection = direction;
            _characterMovementController.LookDirection = lookDirection;

            if (_currentHp <= _lowHp)
                IsHpLow = true;

            if (_currentHp <= 0f)
                Destroy(gameObject);
        }

        protected void OnTriggerEnter(Collider other)
        {
            if (LayerUtils.IsBullet(other.gameObject))          // Проверяем если персонаж сталкивается с пулей
            {
                var bullet = other.gameObject.GetComponent<Bullet>();

                _currentHp -= bullet.Damage;

                Destroy(other.gameObject);
            }
            else if(LayerUtils.IsPickUp(other.gameObject))      // Проверяем если сталкиваемся не с пулей, а с подбираемым объектом
            {
                var pickUp = other.gameObject.GetComponent<PickUpItem>();
                pickUp.PickUp(this);

                Destroy(other.gameObject);                      // Уничтожаем ГО оружия после того как подобрали его
            }
        }

        public void SetWeapon(Weapon weapon)
        {
            _shootingController.SetWeapon(weapon, _hand);
            _currentWeapon = weapon;
        }

        public bool AlreadyHasNewWeapon()
        {
            if (_currentWeapon != _baseWeaponPrefab) 
                return true;
            else return false;
        }

        public void Boost(float boostTime, float boostSpeed)
        {
            _characterMovementController.Boost(boostTime, boostSpeed);
        }

        public void FleeBoost(bool IsFleeing)
        {
            _characterMovementController.FleeBoost(IsFleeing);
        }
    }
}
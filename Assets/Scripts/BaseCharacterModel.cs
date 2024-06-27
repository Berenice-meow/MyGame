using MyGame.Movement;
using MyGame.Shooting;
using MyGame.UI;
using System;
using UnityEngine;

namespace MyGame
{

    public class BaseCharacterModel
    {
        public event Action Dead;
        public event Action OnSpawned;

        public TransformModel Transform {  get; private set; }

        private readonly float _maxHp;
        private readonly float _lowHpCoefficient;

        private float _currentHp;
        private float _lowHp;
        public bool IsHpLow = false;
        private bool _isDead = false;

        public bool IsShooting => _shootingController.HasTarget;

        private readonly IMovementController _characterMovementController;
        private readonly ShootingController _shootingController;
        private readonly CharacterMovementController _characterController;

        public HealthBar healthBar;


        public BaseCharacterModel(IMovementController movementController, ShootingController shootingController, ICharacterConfig config)
        {
            _characterMovementController = movementController;
            _shootingController = shootingController;
            
            _maxHp = config.MaxHp;
            _lowHpCoefficient = config.LowHpCoefficient;

            _currentHp = _maxHp;
            _lowHp = _maxHp * _lowHpCoefficient / 100;

            healthBar.UpdateHealthBar(_maxHp, _currentHp);
        }

        public void Initialize(Vector3 position, Quaternion rotation)
        {
            Transform = new TransformModel(position, rotation);
        }

        public void Move(Vector3 direction)
        {
            var lookDirection = direction;
            if (_shootingController.HasTarget)
                lookDirection = (_shootingController.TargetPosition - Transform.Position).normalized;

            Transform.Position += _characterMovementController.Translate(direction);
            Transform.Rotation = _characterMovementController.Rotate(Transform.Rotation, lookDirection);
        }

        public void Damage(float damage)
        {
            _currentHp -= damage;

            if (_currentHp <= _lowHp)
                IsHpLow = true;

            if (_currentHp <= 0f)
            {
                if (_isDead == false)
                {
                    _isDead = true;
                    //_shootingController.enabled = false;
                    //_characterMovementController.enabled = false;
                    //StartCoroutine(Death());
                }
            }
        }

        public void TryShoot(Vector3 shootPosition)
        {
            _shootingController.TryShoot(shootPosition);
        }
        /*
        public void Spawn(BaseCharacter character)
        {
            OnSpawned?.Invoke(this);
        }
        /*
        IEnumerator Death()
        {
            _animator.SetTrigger("Died");
            _deathSound.Play();
            
            yield return new WaitForSeconds(1.3f);
            Destroy(gameObject);
            GameObject explosion = Instantiate (_exposionParticles, transform.position, transform.rotation);
            
            Dead?.Invoke(this);
            gameObject.GetComponent<BaseCharacter>().Spawn(this);
        }
        */
        
        public void SetWeapon(WeaponModel weapon)
        {
            _shootingController.SetWeapon(weapon);
            //_currentWeapon = weapon;
        }

        /*
        public bool AlreadyHasNewWeapon()
        {
            if (_currentWeapon != _baseWeaponPrefab) 
                return true;
            else return false;
        }
        */
        public void Boost(float boostTime, float boostSpeed)
        {
            _characterController.Boost(boostTime, boostSpeed);
        }

        public void FleeBoost(float fleeSpeed)
        {
            _characterController.FleeBoost(fleeSpeed);
        }
    }
}
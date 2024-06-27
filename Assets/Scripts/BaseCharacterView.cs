using MyGame.Movement;
using MyGame.PickUp;
using MyGame.Shooting;
using System;
using System.Collections;
using UnityEngine;

namespace MyGame
{
    [RequireComponent(typeof(CharacterController), typeof(Animator))]
    public abstract class BaseCharacterView : MonoBehaviour
    {
        public event Action<BaseCharacterView> Dead;
        public event Action<BaseCharacterView> OnSpawned;

        [SerializeField] private WeaponFactory _baseWeapon;
        [SerializeField] private Transform _hand;

        [SerializeField] private GameObject _exposionParticles;
        [SerializeField] private AudioSource _deathSound;
        [SerializeField] private AudioSource _pickUpSound;

        //private float _currentHp;
        //private float _lowHp;
        //public bool IsHpLow = false;
        //private bool _isDead = false;

        private Animator _animator;
        private CharacterController _characterController;
        private IMovementDirectionSource _movementDirectionSource;

        //private Weapon _currentWeapon;
        private WeaponView _weapon;

        //public HealthBar healthBar;

        public BaseCharacterModel Model { get; private set; }

        protected void Awake()
        {
            _animator = GetComponent<Animator>();
            _characterController = GetComponent<CharacterController>();
            _movementDirectionSource = GetComponent<IMovementDirectionSource>();

            //_currentWeapon = _baseWeapon;
        }

        protected void Start()
        {
            SetWeapon(_baseWeapon); 
        }

        public void Initialize(BaseCharacterModel model)
        {
            Model = model;
            Model.Initialize(transform.position, transform.rotation);
            Model.Dead += OnDeath;
            StartCoroutine(Death());
        }

        protected void Update()
        {
            Model.Move(_movementDirectionSource.MovementDirection);
            Model.TryShoot(_weapon.BulletSpawnPosition.position);

            var moveDelta = Model.Transform.Position - transform.position;
            _characterController.Move(moveDelta);
            Model.Transform.Position = transform.position;

            transform.rotation = Model.Transform.Rotation;

            _animator.SetBool("IsMoving", moveDelta != Vector3.zero);
            _animator.SetBool("IsShooting", Model.IsShooting);
            _animator.SetBool("IsBackwards", Mathf.Abs(Mathf.Sign(moveDelta.z) - Mathf.Sign(moveDelta.z)) > Mathf.Epsilon);
        }

        protected void OnDestroy()
        {
            if (Model != null)
                Model.Dead -= OnDeath;
        }

        private void OnDeath()
        {
            Dead?.Invoke(this);
            Destroy(gameObject);
        }

        public void Spawn(BaseCharacterView character)
        {
            OnSpawned?.Invoke(this);
        }

        IEnumerator Death()
        {
            _animator.SetTrigger("Died");
            _deathSound.Play();
            
            yield return new WaitForSeconds(1.3f);
            Destroy(gameObject);
            GameObject explosion = Instantiate (_exposionParticles, transform.position, transform.rotation);
            
            Dead?.Invoke(this);
            gameObject.GetComponent<BaseCharacterView>().Spawn(this);
        }

        protected void OnTriggerEnter(Collider other)
        {
            if (LayerUtils.IsBullet(other.gameObject))
            {
                var bullet = other.gameObject.GetComponent<Bullet>();

                Model.Damage(bullet.Damage);

                Destroy(other.gameObject);
            }
            else if(LayerUtils.IsPickUp(other.gameObject))
            {
                var pickUp = other.gameObject.GetComponent<PickUpItem>();
                _pickUpSound.Play();

                pickUp.PickUp(this);

                Destroy(other.gameObject);
            }
        }

        public void SetWeapon(WeaponFactory weaponFactory)
        {
            if(_weapon != null)
                Destroy(_weapon.gameObject);

            _weapon = weaponFactory.Create(_hand);
            
            Model.SetWeapon(_weapon.Model);

            //_currentWeapon = weapon;
        }

        public bool AlreadyHasNewWeapon()
        {
            if (_weapon != _baseWeapon) 
                return true;
            else return false;
        }
    }
}
using MyGame.Enemy.States;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

namespace MyGame.Movement
{
    [RequireComponent(typeof(CharacterController))]
    public class CharacterMovementController : MonoBehaviour
    {
        private static readonly float SqrEpsilon = Mathf.Epsilon * Mathf.Epsilon;   //Задали статичное неизменяемое поле

        [SerializeField] private float _baseSpeed = 5f;                             // Character's speed

        [SerializeField] private float _currentSpeed;

        [SerializeField] private float _maxRadiansDelta = 10f;                      // Rotation speed
        
        [SerializeField] private float _fleeBoost = 3f;

        private float _fleeSpeed;
        private float _boostTime;
        private float _boostSpeed;

        public Vector3 MovementDirection { get; set; }
        public Vector3 LookDirection { get; set; }

        private CharacterController _characterController;


        protected void Awake()
        {
            _characterController = GetComponent<CharacterController>();

            _currentSpeed = _baseSpeed;
            _fleeSpeed = _currentSpeed + _fleeBoost;
        }

        protected void Update()
        {
            Translate();
            if (_maxRadiansDelta > 0 && LookDirection != Vector3.zero)  //Если есть направление и игрок куда-то двигается, то
                Rotate();                                           //то вызываем метод Rotate (определяем его ниже в коде)
        }

        private void Translate() 
        {
            var delta = MovementDirection * _currentSpeed * Time.deltaTime;    // Time.deltaTime (вспомогательное поле в Юнити) - время, прошедшее с предыдущего кадра

            if (_boostTime > 0)
            {
                _boostTime -= Time.deltaTime;
                delta *= _boostSpeed;
            }

            _characterController.Move(delta);
        }

        private void Rotate()
        {
            var currentLookDirection = transform.rotation * Vector3.forward;        //Получаем текущее направление взгляда. Тут transform - поле, кот. идет от нашего Монобеха
            float sqrMagnitude = (currentLookDirection - LookDirection).sqrMagnitude;   //Смотрим на сколько мы повернулись. Если поворот незначительный, то не учитываем его

            if (sqrMagnitude > SqrEpsilon)
            {
                var newRotation = Quaternion.Slerp(
                    transform.rotation, 
                    Quaternion.LookRotation(LookDirection, Vector3.up),
                    _maxRadiansDelta * Time.deltaTime);

                transform.rotation = newRotation;
            }    
        }

        public void Boost(float boostTime, float boostSpeed)
        {
            _boostTime = boostTime;
            _boostSpeed = boostSpeed;
        }
        
        public void FleeBoost(bool IsFleeing)
        {
            if (IsFleeing == true)
                _currentSpeed = _fleeSpeed;
            else
                _currentSpeed = _baseSpeed;
        }
    }
}
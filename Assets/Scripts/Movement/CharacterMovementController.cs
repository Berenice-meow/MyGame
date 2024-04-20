using UnityEngine;

namespace MyGame.Movement
{
    [RequireComponent(typeof(CharacterController))]
    public class CharacterMovementController : MonoBehaviour
    {
        private static readonly float SqrEpsilon = Mathf.Epsilon * Mathf.Epsilon;   //Задали статичное неизменяемое поле

        [SerializeField]                        // Атрибут, используемый для изменения приватных полей в Инспекторе
        private float _speed = 5f;              // Character's speed
        [SerializeField]
        private float _maxRadiansDelta = 10f;   // Rotation speed

        public Vector3 MovementDirection { get; set; }
        public Vector3 LookDirection { get; set; }

        private CharacterController _characterController;

        private float _boostTime;
        private float _boostSpeed;

        protected void Awake()
        {
            _characterController = GetComponent<CharacterController>();
        }

        protected void Update()
        {
            Translate();
            if (_maxRadiansDelta > 0 && LookDirection != Vector3.zero)  //Если есть направление и игрок куда-то двигается, то
                Rotate();                                           //то вызываем метод Rotate (определяем его ниже в коде)
        }

        private void Translate() 
        {
            var delta = MovementDirection * _speed * Time.deltaTime;    // Time.deltaTime (вспомогательное поле в Юнити) - время, прошедшее с предыдущего кадра

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
    }
}
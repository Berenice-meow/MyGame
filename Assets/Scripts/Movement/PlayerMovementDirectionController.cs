using MyGame.PickUp;
using UnityEngine;

namespace MyGame.Movement
{
    public class PlayerMovementDirectionController : MonoBehaviour, IMovementDirectionSource    //Реализуем Интерфейс iMovementDirection
    {
        [SerializeField] private byte _speedUp = 2;          // Ускорение в n раз

        private UnityEngine.Camera _camera; // Прописываем UnityEngine, чтобы указать что нам нужна именно юнитивская камера

        public Vector3 MovementDirection {  get; private set; }

        protected void Awake()
        {
            _camera = UnityEngine.Camera.main;
        }

        protected void Update()
        {
            var horizontal = Input.GetAxis("Horizontal");   // Получаем Ввод от игрока при помощи класса Input и метода GetAxis с указанием нужной оси
            var vertical = Input.GetAxis("Vertical");       // Влево-вправо : Х, horizontal, Вперед-назад : Z, vertical
       
            var direction = new Vector3(horizontal, 0, vertical);
            direction = _camera.transform.rotation * direction;
            direction.y = 0;

            MovementDirection = direction.normalized;

            if (Input.GetKey(KeyCode.Space))                // Условие для ускорения
                MovementDirection *= _speedUp;    
        }
    }
}
using UnityEditor;
using UnityEngine;

namespace MyGame.PickUp
{
    public class PickUpSpawner : MonoBehaviour
    {
        [SerializeField]
        private PickUpItem _pickUpPrefab;

        [SerializeField]
        private float _range = 4f;                  // радиус зоны спавна

        [SerializeField]
        private int _maxCount = 5;                  // сколько подбираемых предметов мб одновременно в зоне спавна

        //[SerializeField]
        //private float _spawnIntervalSec = 10f;      // интервал между спавном двух предметов

        [SerializeField]
        private float _minSpawnIntervalSec = 1f;

        [SerializeField]
        private float _maxSpawnIntervalSec = 10f;

        private float _spawnIntervalSec;
        private float _currentSpawnTimerSec;
        private int _currentCount;                  // количество заспавненных предметов
        
        protected void Start()
        {
            _spawnIntervalSec = Random.Range(_minSpawnIntervalSec, _maxSpawnIntervalSec);
        }

        protected void Update()
        {
            if (_currentCount < _maxCount)
            {
                _currentSpawnTimerSec += Time.deltaTime;

                if (_currentSpawnTimerSec > _spawnIntervalSec)
                {
                    _spawnIntervalSec = Random.Range(_minSpawnIntervalSec, _maxSpawnIntervalSec);

                    _currentSpawnTimerSec = 0f;
                    _currentCount++;

                    var randomPointInsideRange = Random.insideUnitCircle * _range;      // получаем рандомную точку внутри круга (с радиусом 1) и * на радиус спавнера
                    var randomPosition = new Vector3(randomPointInsideRange.x, 0, randomPointInsideRange.y) + transform.position;

                    var pickUp = Instantiate(_pickUpPrefab, randomPosition, Quaternion.identity, transform);
                    pickUp.OnPickedUp += OnItemPickedUp;    // подписываемся на событие подбора предмета
                }
            }
        }

        private void OnItemPickedUp(PickUpItem pickedUpItem)
        {
            _currentCount--;
            pickedUpItem.OnPickedUp -= OnItemPickedUp;      // отписываемся от события подбора предмета
        }

        protected void OnDrawGizmos()           // напишем собственный гизмос чтобы видеть, где располагается спавнер и какой у него радиус
        {
            var casherColor = Handles.color;                                    // сохраним текущий цвет гизмосов
            Handles.color = Color.green;                                        // установим наш цвет
            Handles.DrawWireDisc(transform.position, Vector3.up, _range);       // нарисуем зону спавна с центром в текущей позиции, нормаль смотрит вверх, радиус _range
            Handles.color = casherColor;
        }
    }
}
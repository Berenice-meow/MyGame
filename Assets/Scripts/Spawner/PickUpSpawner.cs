using MyGame.PickUp;
using UnityEngine;

namespace MyGame.Spawner
{
    public class PickUpSpawner : BaseSpawner
    {
        [SerializeField] private PickUpItem _pickUpPrefab;

        [SerializeField] private int _maxCount = 5;                 // сколько подбираемых предметов мб одновременно в зоне спавна

        private float _currentSpawnTimerSec;
        private int _currentCount;                                  // количество заспавненных предметов

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
    }
}
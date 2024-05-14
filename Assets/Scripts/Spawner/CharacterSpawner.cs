using MyGame.Enemy;
using UnityEditor;
using UnityEngine;

namespace MyGame.Spawner
{
    public class CharacterSpawner : BaseSpawner
    {
        [SerializeField] private PlayerCharacter _player;

        [SerializeField] private EnemyCharacter _enemy;

        [SerializeField] private int _maxCount = 5;                 

        private float _currentSpawnTimerSec;
        private int _currentCount;
        private static bool _isPlayerSpawned;

        private void Awake()
        {
            if (_currentCount < _maxCount)
            {
                _currentSpawnTimerSec += Time.deltaTime;

                if (_currentSpawnTimerSec > _spawnIntervalSec)
                {
                    _spawnIntervalSec = Random.Range(_minSpawnIntervalSec, _maxSpawnIntervalSec);

                    _currentSpawnTimerSec = 0f;
                    _currentCount++;

                    var randomPointInsideRange = Random.insideUnitCircle * _range;
                    var randomPosition = new Vector3(randomPointInsideRange.x, 1, randomPointInsideRange.y) + transform.position;

                    if (_isPlayerSpawned == false && Random.Range(0, 2) == 0)
                    {
                        var player = Instantiate(_player, randomPosition, Quaternion.identity, transform);
                        _isPlayerSpawned = true;
                        player.OnSpawned += OnPlayerSpawned;
                    }
                }
            }
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

                    var randomPointInsideRange = Random.insideUnitCircle * _range;
                    var randomPosition = new Vector3(randomPointInsideRange.x, 1, randomPointInsideRange.y) + transform.position;

                    Instantiate(_enemy, randomPosition, Quaternion.identity, transform);
                }
            }
        }
        
        private void OnPlayerSpawned(PlayerCharacter player)
        {
            _currentCount--;
            player.OnSpawned -= OnPlayerSpawned;
        }

        protected override void OnDrawGizmos()           
        {
            Handles.color = Color.blue;                                        
            Handles.DrawWireDisc(transform.position, Vector3.up, _range);       
        }
    }
}
using MyGame.Enemy;
using UnityEditor;
using UnityEngine;

namespace MyGame.Spawner
{
    public class CharacterSpawner : BaseSpawner
    {
        [SerializeField] private PlayerCharacterView _player;

        [SerializeField] private EnemyCharacterView _enemy;

        [SerializeField] private int _maxCount = 3;                 

        //private float _currentSpawnTimerSec;
        private int _currentCount;

        protected void Awake()
        {  
            for (_currentCount = 0; _currentCount < _maxCount; _currentCount ++)
            {
                var player = FindObjectOfType<PlayerCharacterView>();
                if (!player && Random.Range(0, 2) == 0)
                {
                    var randomPointInsideRange = Random.insideUnitCircle * _range;
                    var randomPosition = new Vector3(randomPointInsideRange.x, 1, randomPointInsideRange.y) + transform.position;

                    var character = Instantiate(_player, randomPosition, Quaternion.identity);
                    character.OnSpawned += OnCharacterSpawned;
                }
                else
                {
                    var randomPointInsideRange = Random.insideUnitCircle * _range;
                    var randomPosition = new Vector3(randomPointInsideRange.x, 1, randomPointInsideRange.y) + transform.position;

                    var character = Instantiate(_enemy, randomPosition, Quaternion.identity);
                    character.OnSpawned += OnCharacterSpawned;
                }
            }
        }

        /*
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

                    var character = Instantiate(_enemy, randomPosition, Quaternion.identity);
                    character.OnSpawned += OnCharacterSpawned;
                }
            }
        }
        */

        private void OnCharacterSpawned(BaseCharacter character)
        {
            _currentCount--;
            character.OnSpawned -= OnCharacterSpawned;
        }

        protected override void OnDrawGizmos()           
        {
            Handles.color = Color.blue;                                        
            Handles.DrawWireDisc(transform.position, Vector3.up, _range);       
        }
    }
}
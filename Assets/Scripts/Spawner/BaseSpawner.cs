using UnityEditor;
using UnityEngine;

namespace MyGame.Spawner
{
    public class BaseSpawner : MonoBehaviour
    {
        [SerializeField] protected float _range = 5f;                  // радиус зоны спавна

        [SerializeField] protected float _minSpawnIntervalSec = 1f;

        [SerializeField] protected float _maxSpawnIntervalSec = 10f;

        protected float _spawnIntervalSec;
        
        protected void Start()
        {
            _spawnIntervalSec = Random.Range(_minSpawnIntervalSec, _maxSpawnIntervalSec);
        }

        protected virtual void OnDrawGizmos()           // напишем собственный гизмос чтобы видеть, где располагается спавнер и какой у него радиус
        {
            var casherColor = Handles.color;                                    // сохраним текущий цвет гизмосов
            Handles.color = Color.green;                                        // установим наш цвет
            Handles.DrawWireDisc(transform.position, Vector3.up, _range);       // нарисуем зону спавна с центром в текущей позиции, нормаль смотрит вверх, радиус _range
            Handles.color = casherColor;
        }
    }
}
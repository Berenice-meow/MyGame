using MyGame.FSM;
using UnityEngine;

namespace MyGame.Enemy.States
{
    public class FindWayState : BaseState
    {
        private const float MaxDistanceBetweenRealAndCalculated = 3f;
        
        private readonly EnemyTarget _target;
        private readonly NavMesher _navMesher;
        private readonly EnemyDirectionController _enemyDirectionController;

        private Vector3 _currentPoint;

        public FindWayState (EnemyTarget target, NavMesher navMesher, EnemyDirectionController enemyDirectionController)        // Создаем конструктор, куда передаем цель врага
        {
            _target = target;
            _navMesher = navMesher;
            _enemyDirectionController = enemyDirectionController;
        }

        public override void Execute()
        {
            Vector3 targetPosition = _target.Closest.transform.position;
                        
            if ( !_navMesher.IsPathCalculated || _navMesher.DistanceToTargetPointFrom(targetPosition) > MaxDistanceBetweenRealAndCalculated )
                _navMesher.CalculatePath(targetPosition);       // если изначальаная точка, к которой мы ведем персонажа, сильно отличается от текущей точки интереса -> перерасчитываем путь

            var currentPoint = _navMesher.GetCurrentPoint();
            
            if(_currentPoint != currentPoint)
            {
                _currentPoint = currentPoint;
                _enemyDirectionController.UpdateMovementDirection(currentPoint);
            }
        }   
    }
}

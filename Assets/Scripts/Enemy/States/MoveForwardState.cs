using MyGame.FSM;
using UnityEngine;

namespace MyGame.Enemy.States
{
    public class MoveForwardState : BaseState           // данное состояние вкл когда мы подошли близко к цели -> откл поиск пути по NavMesh и просто идем вперед к цели 
    {
        private readonly EnemyTarget _target;
        private readonly EnemyDirectionController _enemyDirectionController;

        private Vector3 _currentPoint;

        public MoveForwardState(EnemyTarget target, EnemyDirectionController enemyDirectionController)
        {
            _target = target;
            _enemyDirectionController = enemyDirectionController;
        }

        public override void Execute()
        {
            if (_target.Closest != null)
            {
                Vector3 targetPosition = _target.Closest.transform.position;

                if (_currentPoint != targetPosition)
                {
                    _currentPoint = targetPosition;
                    _enemyDirectionController.UpdateMovementDirection(targetPosition);
                }
            }
        }
    }
}

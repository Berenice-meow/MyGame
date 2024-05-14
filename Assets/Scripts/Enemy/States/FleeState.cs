using MyGame.FSM;
using UnityEngine;

namespace MyGame.Enemy.States
{
    public class FleeState : BaseState
    {
        private readonly EnemyTarget _target;
        private readonly EnemyDirectionController _enemyDirectionController;
        private readonly BaseCharacter _baseCharacter;

        private Vector3 _currentPoint;

        private float _fleeSpeed = 7f;

        public FleeState(EnemyTarget target, EnemyDirectionController enemyDirectionController, BaseCharacter baseCharacter)
        {
            _target = target;
            _enemyDirectionController = enemyDirectionController;
            _baseCharacter = baseCharacter;
        }

        public override void Execute()
        {
            Vector3 targetPosition = _target.Closest.transform.position;

            if (_currentPoint != targetPosition)
            {
                _currentPoint = targetPosition;
                _enemyDirectionController.UpdateMovementDirection(-targetPosition);

                _baseCharacter.FleeBoost(_fleeSpeed); 
            }
        }
    }
}

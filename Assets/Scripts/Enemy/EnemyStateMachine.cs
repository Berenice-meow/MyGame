using MyGame.Enemy.States;
using MyGame.FSM;
using System.Collections.Generic;
using UnityEngine;

namespace MyGame.Enemy
{
    public class EnemyStateMachine : BaseStateMachine
    {
        private const float NavMeshTurnOffDistance = 5f;

        [SerializeField]
        private int _fleeChance = 70;

        public EnemyStateMachine (EnemyDirectionController enemyDirectionController, NavMesher navMesher, EnemyTarget target, BaseCharacter baseCharacter)
        {
            var idleState = new IdleState (baseCharacter);
            var findWayState = new FindWayState (target, navMesher, enemyDirectionController);
            var moveForwardState = new MoveForwardState (target, enemyDirectionController);
            var fleeState = new FleeState(target, enemyDirectionController, baseCharacter);

            SetInitialState(idleState);

            AddState(state: idleState, transitions: new List<Transition>
                {
                    new Transition(
                        findWayState,
                        () => target.DistanceToClosestFromAgent() > NavMeshTurnOffDistance),
                    new Transition(
                        moveForwardState,
                        () => target.DistanceToClosestFromAgent() <= NavMeshTurnOffDistance),
                    new Transition(
                        fleeState,
                        () => baseCharacter.IsHpLow == true 
                        && Random.Range(0, 100) < _fleeChance 
                        && target.IsTargetCharacter() == true),
                }
            );

            AddState(state: findWayState, transitions: new List<Transition>
                {
                    new Transition(
                        idleState,
                        () => target.Closest == null),
                    new Transition(
                        moveForwardState,
                        () => target.DistanceToClosestFromAgent() <= NavMeshTurnOffDistance),
                }
            );

            AddState(state: moveForwardState, transitions: new List<Transition>
                {
                    new Transition(
                        idleState,
                        () => target.Closest == null),
                    new Transition(
                        findWayState,
                        () => target.DistanceToClosestFromAgent() > NavMeshTurnOffDistance),
                    new Transition(
                        fleeState,
                        () => baseCharacter.IsHpLow == true 
                        && Random.Range(0, 100) < _fleeChance 
                        && target.IsTargetCharacter() == true),
                }
            );

            AddState(state: fleeState, transitions: new List<Transition>
                {
                    new Transition(
                        idleState,
                        () => target.IsTargetCharacter() == false),
                }
            );
        }
    }
}

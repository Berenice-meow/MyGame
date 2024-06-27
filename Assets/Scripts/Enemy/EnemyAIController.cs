using MyGame.Movement;
using System.Collections;
using UnityEngine;

namespace MyGame.Enemy
{
    public class EnemyAIController : MonoBehaviour
    {
        [SerializeField] private float _viewRadius = 100f;
        
        private EnemyTarget _target;
        private EnemyStateMachine _stateMachine;

        protected void Awake()
        {
            var player = FindObjectOfType<PlayerCharacterView>();

            var enemy = FindObjectOfType<EnemyCharacterView>();

            var enemyDirectionController = GetComponent<EnemyDirectionController>();

            var navMesher = new NavMesher(transform);

            _target = new EnemyTarget(transform, player, _viewRadius, enemy);

            var baseCharacter = GetComponent<BaseCharacter>();

            _stateMachine = new EnemyStateMachine(enemyDirectionController, navMesher, _target, baseCharacter);
        }

        protected void Update()
        {
            _target.FindClosest();
            _stateMachine.Update();
        }
    }
}
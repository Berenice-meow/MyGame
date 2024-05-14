using UnityEngine;

namespace MyGame.Enemy
{
    public class EnemyTarget
    {
        public GameObject Closest {  get; private set; }        // Получаем ГО ближайшей цели

        private readonly float _viewRadius;
        private readonly Transform _agentTransform;
        private readonly PlayerCharacter _player;
        private readonly EnemyCharacter _agent;

        private readonly Collider[] _colliders = new Collider[10];

        public EnemyTarget(Transform agentTransform, PlayerCharacter player, float viewRadius, EnemyCharacter enemy)
        {
            _agentTransform = agentTransform;
            _player = player;
            _viewRadius = viewRadius;
            _agent = enemy;
        }

        public void FindClosest()
        {            
            float minDistance = float.MaxValue;

            if (_agent.AlreadyHasNewWeapon() == false)
            {
                var count = FindAllTargets(LayerUtils.WeaponsMask);

                for (int i = 0; i < count; i++)
                {
                    var go = _colliders[i].gameObject;
                    var distance = DistanceFromAgentTo(go);
                    if (distance < minDistance)
                    {
                        minDistance = distance;
                        Closest = go;
                    }
                }
            }
            
            else
            {
                var count = FindAllTargets(LayerUtils.BonusesMask | LayerUtils.CharactersMask);         // находим все цели среди подбираемых айтемов и других персонажей

                for (int i = 0; i < count; i++)                    // проходим по всем найденным целям
                {
                    var go = _colliders[i].gameObject;
                    if (go == _agentTransform.gameObject)           // проверяем что мы не цель для себя
                        continue;
                    var distance = DistanceFromAgentTo(go);
                    if (distance < minDistance)
                    {
                        minDistance = distance;
                        Closest = go;
                    }
                }
            }

            if (_player != null && DistanceFromAgentTo(_player.gameObject) < minDistance)           // охотимся на игрока если рядом ничего другого нет
                Closest = _player.gameObject;
        }

        public float DistanceToClosestFromAgent()
        {
            if (Closest != null)
                DistanceFromAgentTo(Closest);

            return 0;
        }

        private int FindAllTargets(int layerMask)               // метод возвращающий количество найденных целей
        {
            var size = Physics.OverlapSphereNonAlloc(
                _agentTransform.position,
                _viewRadius,
                _colliders,
                layerMask);

            return size;
        }

        private float DistanceFromAgentTo(GameObject go) => (_agentTransform.position - go.transform.position).magnitude;

        public bool IsTargetCharacter()
        {
            if (LayerUtils.IsCharacter(Closest) && Closest != null)
                return true;
            else return false;
        }
    }
}
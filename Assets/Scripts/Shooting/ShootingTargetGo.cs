using UnityEngine;

namespace MyGame.Shooting
{
    public class ShootingTargetGo : IShootingTarget
    {
        private readonly Collider[] _colliders = new Collider[2];
        private readonly GameObject _shooter;

        public ShootingTargetGo(GameObject shooter)
        {
            _shooter = shooter;
        }

        public BaseCharacterModel GetTarget(Vector3 position, float radius)
        {
            BaseCharacterModel target = null;

            var mask = LayerUtils.CharactersMask;

            var size = Physics.OverlapSphereNonAlloc(position, radius, _colliders, mask);
            if (size > 1)
            {
                for (int i = 0; i < size; i++)
                {
                    if (_colliders[i].gameObject != _shooter)
                    {
                        target = _colliders[i].gameObject.GetComponent<BaseCharacterView>().Model;
                        break;
                    }
                }
            }
            return target;
        }
    }
}

using UnityEngine;

namespace MyGame.Shooting
{
    public interface IShootingTarget
    {
        BaseCharacterModel GetTarget(Vector3 position, float radius);       
    }
}

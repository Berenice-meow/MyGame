using UnityEngine;

namespace MyGame.PickUp
{
    public class PickUpBoost : PickUpItem
    {
        [SerializeField] private float _boostTime = 10f;
        [SerializeField] private float _boostSpeed = 2f;

        public override void PickUp(BaseCharacter character)
        {
            base.PickUp(character);
            character.Boost(_boostTime, _boostSpeed);
        }
    }
}
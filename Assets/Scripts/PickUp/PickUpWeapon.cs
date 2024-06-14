using MyGame.Shooting;
using UnityEngine;

namespace MyGame.PickUp
{
    public class PickUpWeapon : PickUpItem
    {
        [SerializeField] private Weapon _weaponPrefab;

        public override void PickUp(BaseCharacter character)
        {
            base.PickUp(character);
            character.SetWeapon(_weaponPrefab);
        }
    }
}
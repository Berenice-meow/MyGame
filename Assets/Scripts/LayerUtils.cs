using UnityEngine;

namespace MyGame
{
    public static class LayerUtils
    {
        public const string BulletLayerName = "Bullet";     // 3 layers: default player enemy -> 1 1 0
        //public const string PickUpLayerName = "PickUp";
        public const string EnemyLayerName = "Enemy";       // Enemy Mask -> 0 0 1
        public const string PlayerLayerName = "Player";
        public const string WeaponLayerName = "Weapon";
        public const string BonusLayerName = "Bonus";

        public static readonly int BulletLayer = LayerMask.NameToLayer(BulletLayerName);
        //public static readonly int PickUpLayer = LayerMask.NameToLayer(PickUpLayerName);
        public static readonly int EnemyLayer = LayerMask.NameToLayer(EnemyLayerName);
        public static readonly int PlayerLayer = LayerMask.NameToLayer(PlayerLayerName);
        public static readonly int WeaponLayer = LayerMask.NameToLayer(WeaponLayerName);
        public static readonly int BonusLayer = LayerMask.NameToLayer(BonusLayerName);

        public static readonly int CharactersMask = LayerMask.GetMask(EnemyLayerName, PlayerLayerName);   // PlayerLayerName - можем целиться по игроку
        //public static readonly int PickUpsMask = LayerMask.GetMask(WeaponLayerName, BonusLayerName);
        public static readonly int WeaponsMask = LayerMask.GetMask(WeaponLayerName);
        public static readonly int BonusesMask = LayerMask.GetMask(BonusLayerName);

        public static bool IsBullet(GameObject other) => other.layer == BulletLayer;
        public static bool IsPickUp(GameObject other) => other.layer == WeaponLayer || other.layer == BonusLayer;
        public static bool IsWeapon(GameObject other) => other.layer == WeaponLayer;
        public static bool IsCharacter(GameObject other) => other.layer == EnemyLayer || other.layer == PlayerLayer;

        /*
        Запись выше - лямбда-запись, сокращение записиси, приведенной ниже:

        public static bool IsBullet(GameObject other) 
        {
            return other.layer == BulletLayer;
        }
        */
    }
}

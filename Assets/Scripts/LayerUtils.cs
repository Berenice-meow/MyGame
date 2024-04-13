using UnityEngine;

namespace MyGame
{
    public static class LayerUtils
    {
        public const string BulletLayerName = "Bullet";     // 3 layers: default player enemy -> 1 1 0
        public const string EnemyLayerName = "Enemy";       // Enemy Mask -> 0 0 1
        public const string PlayerLayerName = "Player";

        public static readonly int BulletLayer = LayerMask.NameToLayer(BulletLayerName);

        public static readonly int AimMask = LayerMask.GetMask(EnemyLayerName, PlayerLayerName);   // PlayerLayerName - можем целиться по игроку

        public static bool IsBullet(GameObject other) => other.layer == BulletLayer;
        /*
        Запись выше - лямбда-запись, сокращение записиси, приведенной ниже:

        public static bool IsBullet(GameObject other) 
        {
            return other.layer == BulletLayer;
        }
        */
    }
}

using MyGame.Movement;
using MyGame.PickUp;
using System;
using UnityEngine;

namespace MyGame
{
    [RequireComponent(typeof(PlayerMovementDirectionController))]
    public class PlayerCharacter : BaseCharacter
    {
        public event Action<PlayerCharacter> OnSpawned;

        public void Death()
        {
            OnSpawned?.Invoke(this);
        }
    }
}
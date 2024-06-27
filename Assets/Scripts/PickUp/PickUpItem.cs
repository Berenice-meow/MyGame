using System;
using UnityEngine;

namespace MyGame.PickUp
{
    public abstract class PickUpItem : MonoBehaviour
    {
        public event Action<PickUpItem> OnPickedUp;

        public virtual void PickUp(BaseCharacterView character)
        {
            OnPickedUp?.Invoke(this);
        }
    }
}
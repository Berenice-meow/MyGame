using System;
using UnityEngine;

namespace MyGame.PickUp
{
    public abstract class PickUpItem : MonoBehaviour
    {
        public event Action<PickUpItem> OnPickedUp;

        public virtual void PickUp(BaseCharacter character)
        {
            OnPickedUp?.Invoke(this);

            /* 
            Запись выше равноценна следующей записи:
            if (OnPickedUp != null)
                OnPickedUp.Invoke(this);
            */ 
        }
    }
}
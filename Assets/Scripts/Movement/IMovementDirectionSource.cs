using UnityEngine;

namespace MyGame.Movement
{
    public interface IMovementDirectionSource   //Получаем направление движения через Интерфейс, чтобы м/б разные Source (устройства ввода): клавамышь, экран, джойстик
    {
        Vector3 MovementDirection { get; }
    }
}

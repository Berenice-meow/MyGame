using UnityEngine;

namespace MyGame.Movement
{
    public interface IMovementController
    {
        Vector3 Translate (Vector3 movementDirection);

        Quaternion Rotate (Quaternion currentRotation, Vector3 lookDirection);
    }
}

using UnityEngine;

namespace MyGame.Timer
{
    public class UnityTimer : ITimer
    {
        public float DeltaTime => Time.deltaTime;
    }
}

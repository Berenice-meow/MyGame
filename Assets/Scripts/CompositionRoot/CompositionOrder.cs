using MyGame.Timer;
using System.Collections.Generic;
using UnityEngine;

namespace MyGame.CompositionRoot
{
    public class CompositionOrder : MonoBehaviour
    {
        [SerializeField] private List<CompositionRoot> _order;
        private void Awake()
        {
            ITimer timer = new UnityTimer();

            foreach (var compositionRoot in _order)
            {
                compositionRoot.Compose(timer);
            }
        }
    }
}

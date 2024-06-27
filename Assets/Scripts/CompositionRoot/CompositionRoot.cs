using MyGame.Timer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace MyGame.CompositionRoot
{
    public abstract class CompositionRoot : MonoBehaviour
    {
        public abstract void Compose(ITimer timer);
    }
}

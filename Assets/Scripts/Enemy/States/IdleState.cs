using MyGame.FSM;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

namespace MyGame.Enemy.States
{
    public class IdleState : BaseState
    {
        private readonly BaseCharacter _baseCharacter;

        private bool IsFleeing = false;

        public IdleState(BaseCharacter baseCharacter)
        {
            _baseCharacter = baseCharacter;
        }

        public override void Execute()
        {
            _baseCharacter.FleeBoost(IsFleeing);
        }
    }
}


using MyGame.FSM;

namespace MyGame.Enemy.States
{
    public class IdleState : BaseState
    {
        private readonly BaseCharacter _baseCharacter;

        private float _fleeSpeed = 0f;

        public IdleState(BaseCharacter baseCharacter)
        {
            _baseCharacter = baseCharacter;
        }

        public override void Execute()
        {
            _baseCharacter.FleeBoost(_fleeSpeed);
        }
    }
}


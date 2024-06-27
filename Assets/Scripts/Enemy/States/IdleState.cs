using MyGame.FSM;

namespace MyGame.Enemy.States
{
    public class IdleState : BaseState
    {
        private readonly BaseCharacterModel _baseCharacter;

        private float _fleeSpeed = 0f;

        public IdleState(BaseCharacterModel baseCharacter)
        {
            _baseCharacter = baseCharacter;
        }

        public override void Execute()
        {
            _baseCharacter.FleeBoost(_fleeSpeed);
        }
    }
}


using MyGame.Movement;
using MyGame.Shooting;
using MyGame.Timer;
using UnityEngine;


namespace MyGame.CompositionRoot
{
    public class CharacterCompositionRoot : CompositionRoot
    {
        [SerializeField] private CharacterConfig _characterConfig;
        [SerializeField] private BaseCharacterView _view; 

        public override void Compose(ITimer timer)
        {
            IMovementController movementController = new CharacterMovementController(_characterConfig, timer);
            IShootingTarget shootingTarget = new ShootingTargetGo(_view.gameObject);
            var shootingController = new ShootingController(shootingTarget, timer);

            var character = new BaseCharacterModel(movementController, shootingController, _characterConfig);
            _view.Initialize(character);
        }
    }
}

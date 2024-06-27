using MyGame.Timer;
using UnityEngine;

namespace MyGame.Movement
{
    public class CharacterMovementController : IMovementController
    {
        private static readonly float SqrEpsilon = Mathf.Epsilon * Mathf.Epsilon;

        private readonly ITimer _timer;

        private readonly float _maxRadiansDelta;
        private readonly float _baseSpeed;

        private float _currentSpeed;
        
        private float _boostTime;
        private float _boostSpeed;


        public CharacterMovementController(ICharacterConfig config, ITimer timer)
        {
            _maxRadiansDelta = config.MaxRadiansDelta;
            _baseSpeed = config.BaseSpeed;
            
            _currentSpeed = _baseSpeed;

            _timer = timer;
        }

        public Vector3 Translate(Vector3 movementDirection) 
        {
            var delta = movementDirection * _currentSpeed * _timer.DeltaTime;   
            if (_boostTime > 0)
            {
                _boostTime -= _timer.DeltaTime;
                delta *= _boostSpeed;
            }  
            return delta;
        }

        public Quaternion Rotate(Quaternion currentRotation, Vector3 lookDirection)
        {
            if (_maxRadiansDelta > 0 && lookDirection != Vector3.zero)
            {
                var currentLookDirection = currentRotation * Vector3.forward;
                float sqrMagnitude = (currentLookDirection - lookDirection).sqrMagnitude; 

                if (sqrMagnitude > SqrEpsilon)
                {
                    var newRotation = Quaternion.Slerp(
                        currentRotation,
                        Quaternion.LookRotation(lookDirection, Vector3.up),
                        _maxRadiansDelta * _timer.DeltaTime);

                    return newRotation;
                }
            }
            return currentRotation;
        }

        public void Boost(float boostTime, float boostSpeed)
        {
            _boostTime = boostTime;
            _boostSpeed = boostSpeed;
        }
        
        public void FleeBoost(float fleeSpeed)
        {
            if (fleeSpeed != 0)
                _currentSpeed = fleeSpeed;
            else
                _currentSpeed = _baseSpeed;
        }
    }
}
using System;
using UnityEngine;

namespace MyGame.Camera
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private Vector3 _followCameraOffset = Vector3.zero;
        [SerializeField] private Vector3 _rotationOffset = Vector3.zero;

        [SerializeField] private PlayerCharacterView _player;

        
        protected void Awake()
        {
            if (_player == null)
                throw new NullReferenceException($"Follow camera can't follow null player - {nameof(_player)}");
        }
        

        protected void LateUpdate()
        {
            if (_player == null)
                _player = FindObjectOfType<PlayerCharacterView>();
            else
            {
                Vector3 targetRotate = _rotationOffset - _followCameraOffset;

                transform.position = _player.transform.position + _followCameraOffset;
                transform.rotation = Quaternion.LookRotation(targetRotate, Vector3.up);
            } 
        }
    }
}
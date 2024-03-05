using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GameManager
{
    public class GameManager : MonoBehaviour
    {
        public bool debugMode; // Turn OFF when shipping.
        private AudioSource _audioSource;

        private void Start()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        public void OnMute(InputAction.CallbackContext context)
        {
            if (!context.performed) return;
            if (!_audioSource.mute) _audioSource.mute = true;
            else _audioSource.mute = false;
        }
    }
}


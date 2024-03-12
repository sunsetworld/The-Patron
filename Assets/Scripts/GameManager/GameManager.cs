using UnityEngine;
using UnityEngine.InputSystem;

namespace GameManager
{
    public class GameManager : MonoBehaviour
    {
        public bool debugMode; // Turn OFF when shipping.
        private AudioSource _audioSource;
        public bool playerHasKey;

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

        public void PlayMusic()
        {
            if (_audioSource.mute) _audioSource.mute = false;
            if (!_audioSource.isPlaying) _audioSource.Play();
        }

        public void OnFullscreen(InputAction.CallbackContext context)
        {
            if (context.performed) Screen.fullScreen = !Screen.fullScreen;
        }
    }
}


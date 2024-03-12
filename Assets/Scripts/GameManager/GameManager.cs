using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GameManager
{
    public class GameManager : MonoBehaviour
    {
        public bool debugMode; // Turn OFF when shipping.
        private AudioSource _audioSource;
        public bool playerHasKey;

        [SerializeField] private TMP_FontAsset openDyslexicFont;
        [SerializeField] private TMP_FontAsset standardFont;

        private bool _useOpenDyslexic;

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

        public void OnFullscreen(InputAction.CallbackContext context)
        {
            if (context.performed) Screen.fullScreen = !Screen.fullScreen;
        }
    }
}


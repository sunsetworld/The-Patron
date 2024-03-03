using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField, Tooltip("Player Movement")] private float playerSpeed = 20f;
        private bool _canMove;
        private Vector2 _playerInput;
        [SerializeField, Range(1f, 20f)] private float playerJumpHeight = 5f;
        [Tooltip("Components")] private Rigidbody2D _rigidbody2D;
        private SpriteRenderer _spriteRenderer;
        private AudioSource _playerAudioSource;
        [SerializeField] private AudioClip[] movementSounds;
        private bool _canDoubleJump;
        private bool _canJump;
        private bool _isTouchingFloor;
        [SerializeField] private AudioClip jumpSound;
        // Start is called before the first frame update
        void Start()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _playerAudioSource = GetComponent<AudioSource>();
        }
        private void FixedUpdate()
        {
            if (_canMove) _rigidbody2D.velocity += _playerInput * (playerSpeed * Time.fixedDeltaTime);
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            _playerInput = context.ReadValue<Vector2>();
            if (context.performed)
            {
                _canMove = true;
                ChooseMovementSound();
            }
            else if (context.canceled)
            {
                _playerAudioSource.Stop();
                _canMove = false;
            }
            SetPlayerSpriteDirection();
        }
        void ChooseMovementSound()
        {
            if (!_isTouchingFloor)
            {
                _playerAudioSource.Stop();
                return;
            }
            if (_playerAudioSource.isPlaying) _playerAudioSource.Stop();
            int newSound = Random.Range(0, movementSounds.Length);
            _playerAudioSource.clip = movementSounds[newSound];
            Debug.Log("Player Movement Clip: " + _playerAudioSource.clip.name);
            _playerAudioSource.Play();
        }
        void SetPlayerSpriteDirection()
        {
            if (_playerInput.x < 0) _spriteRenderer.flipX = true;
            else if (_playerInput.x > 0) _spriteRenderer.flipX = false;
        }
        public void OnJump(InputAction.CallbackContext context)
        {
            if (!context.performed) return;
            if (_canJump || _canDoubleJump)
            {
                Vector2 jumpVelocity = _rigidbody2D.velocity;
                jumpVelocity.y += playerJumpHeight;
                _rigidbody2D.velocity = jumpVelocity;
                AudioSource.PlayClipAtPoint(jumpSound, transform.position);
                if (_canDoubleJump) _canDoubleJump = false;
            }
        }
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Lightbulb")) return;
            if (!_isTouchingFloor)_isTouchingFloor = true;
            _canJump = true;
        }
        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Lightbulb")) return;
            if(_isTouchingFloor)_isTouchingFloor = false;
            _canJump = false;
            _canDoubleJump = true;
        }
    }
}


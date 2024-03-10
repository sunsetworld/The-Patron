using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Jim
{
    public class JimMovement : MonoBehaviour
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

        [Header("Jim Specific")] 
        public bool activateJim;
        public bool canUseJim;

        [SerializeField] private float arrowLength = 3;
        [SerializeField] public GameObject characterArrowObj;
        
        // Start is called before the first frame update
        void Start()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _playerAudioSource = GetComponent<AudioSource>();
        }
        private void FixedUpdate()
        {
            JimMove();
        }

        void JimMove()
        {
            if (!activateJim) return;
            if (!canUseJim) return;
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
            _playerAudioSource.Play();
        }
        void SetPlayerSpriteDirection()
        {
            if (!activateJim) return;
            if (!canUseJim) return;
            if (_playerInput.x < 0) _spriteRenderer.flipX = true;
            else if (_playerInput.x > 0) _spriteRenderer.flipX = false;
        }
        public void OnJump(InputAction.CallbackContext context)
        {
            if (!context.performed) return;
            if (!activateJim) return;
            if (!canUseJim) return;
            Vector2 jumpVelocity = _rigidbody2D.velocity;
            jumpVelocity.y += playerJumpHeight;
            _rigidbody2D.velocity = jumpVelocity;
            AudioSource.PlayClipAtPoint(jumpSound, transform.position);
        }

        public IEnumerator ShowCharacterArrow()
        {
            characterArrowObj.SetActive(true);
            yield return new WaitForSeconds(arrowLength);
            characterArrowObj.SetActive(false);
        }
    }
}



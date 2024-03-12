using System.Collections;
using GameHUD;
using Jim;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [Header("Run")]
        [SerializeField, Range(1f, 50f)] private float playerSpeed = 20f;
        private bool _canMove;
        private Vector2 _playerInput;
        
        [Header("Jump")]
        [SerializeField, Range(1f, 20f)] private float playerJumpHeight = 5f;
        private bool _canDoubleJump;
        private bool _canJump;
        
        [Header("Audio")]
        [SerializeField] private AudioClip jumpSound;
        [SerializeField] private AudioClip[] movementSounds;
        
        [Header("Internal Components")]
        private Rigidbody2D _rigidbody2D;
        private SpriteRenderer _spriteRenderer;
        private AudioSource _playerAudioSource;
        private Animator _animator;
        
        [Header("External Components")]
        private JimMovement _jimMovement;
        private HUD _hud;
        private CharacterSwap _characterSwap;
        
        [Header("General Movement")]
        private bool _isTouchingFloor;
        public float downwardVelocity;

        [Header("Character Arrow")] 
        [SerializeField] private GameObject characterArrowObj;
        [SerializeField] private float arrowLength = 3;

        
        // Start is called before the first frame update
        void Start()
        {
            _jimMovement = FindObjectOfType<JimMovement>();
            _hud = FindObjectOfType<HUD>();
            _characterSwap = FindObjectOfType<CharacterSwap>();
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _playerAudioSource = GetComponent<AudioSource>();
            _animator = GetComponent<Animator>();
        }
        private void FixedUpdate()
        {
            if (_canMove && !_jimMovement.canUseJim) 
                _rigidbody2D.velocity += _playerInput * (playerSpeed * Time.fixedDeltaTime);
            Debug.Log("Player Velocity: " + _rigidbody2D.velocity);
            RecordDownwardVelocity();
        }

        void RecordDownwardVelocity()
        {
            if (_rigidbody2D.velocity.y < 0) downwardVelocity = _rigidbody2D.velocity.y;
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            _playerInput = context.ReadValue<Vector2>();
            if (context.performed)
            {
                _canMove = true;
                _animator.SetBool("isMoving", true);
                ChooseMovementSound();
            }
            else if (context.canceled)
            {
                _playerAudioSource.Stop();
                _animator.SetBool("isMoving", false);
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
            // Debug.Log("Player Movement Clip: " + _playerAudioSource.clip.name);
            _playerAudioSource.Play();
        }
        void SetPlayerSpriteDirection()
        {
            if (_jimMovement.canUseJim) return;
            if (_playerInput.x < 0) _spriteRenderer.flipX = true;
            else if (_playerInput.x > 0) _spriteRenderer.flipX = false;
        }
        public void OnJump(InputAction.CallbackContext context)
        {
            if (!context.performed) return;
            if (_jimMovement.canUseJim) return;
            if (_canJump || _canDoubleJump)
            {
                Vector2 jumpVelocity = _rigidbody2D.velocity;
                jumpVelocity.y += playerJumpHeight;
                _rigidbody2D.velocity = jumpVelocity;
                AudioSource.PlayClipAtPoint(jumpSound, transform.position);
                if (_canDoubleJump) _canDoubleJump = false;
            }
        }

        public void OnSwitch(InputAction.CallbackContext context)
        {
            if (!context.performed) return;
            if (!_jimMovement.activateJim) return;
            if (!_characterSwap.firstSwap)
            {
                _characterSwap.firstSwap = true;
                _characterSwap.DisableCanvas();
            }
            _jimMovement.canUseJim = !_jimMovement.canUseJim;
            if (_jimMovement.canUseJim)
            {
                StopCoroutine(ShowCharacterArrow());
                characterArrowObj.SetActive(false);
                StartCoroutine(_jimMovement.ShowCharacterArrow());
            }
            else
            {
                StopCoroutine(_jimMovement.ShowCharacterArrow());
                _jimMovement.characterArrowObj.SetActive(false);
                StartCoroutine(ShowCharacterArrow());
            }
            _hud.SwitchCharacter();
            
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

        public IEnumerator ShowCharacterArrow()
        {
            characterArrowObj.SetActive(true);
            yield return new WaitForSeconds(arrowLength);
            characterArrowObj.SetActive(false);
        }
    }
}


using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField, Range(1f, 50f), Tooltip("Player Movement")] private float playerSpeed = 20f;
        private bool _canMove;
        private Vector2 _playerInput;
        [SerializeField, Range(1f, 20f)] private float playerJumpHeight = 5f;
        [Tooltip("Components")] private Rigidbody2D _rigidbody2D;
        private SpriteRenderer _spriteRenderer;


        private bool _canJump;
        // Start is called before the first frame update
        void Start()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }
        private void FixedUpdate()
        {
            if (_canMove) _rigidbody2D.velocity += _playerInput * (playerSpeed * Time.fixedDeltaTime);
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            _playerInput = context.ReadValue<Vector2>();
            Debug.Log("Player Input" + _playerInput);
            if (context.performed) _canMove = true;
            else if (context.canceled)
            {
                // _rigidbody2D.velocity = Vector2.zero;
                _canMove = false;
            }
            SetPlayerSpriteDirection();
        }

        void SetPlayerSpriteDirection()
        {
            if (_playerInput.x < 0) _spriteRenderer.flipX = true;
            else if (_playerInput.x > 0) _spriteRenderer.flipX = false;
        }

        public void OnJump(InputAction.CallbackContext context)
        {
            if (context.performed && _canJump)
            {
                Vector2 jumpVelocity = _rigidbody2D.velocity;
                jumpVelocity.y += playerJumpHeight;
                _rigidbody2D.velocity = jumpVelocity;
            }
        }
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Lightbulb")) return;
            _canJump = true;
        }
        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Lightbulb")) return;
            _canJump = false;
        }
    }
}


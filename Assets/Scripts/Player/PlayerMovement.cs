    using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float playerSpeed = 20f;
        private bool _canMove;
        private Rigidbody2D _rigidbody2D;
        private Vector2 _playerInput;
        [SerializeField] private float playerJumpHeight = 5f;

        private bool _canJump;
        // Start is called before the first frame update
        void Start()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            Debug.Log("Can Player Jump" + _canJump);
        }

        private void FixedUpdate()
        {
            if (_canMove) _rigidbody2D.velocity += _playerInput * (playerSpeed * Time.fixedDeltaTime);
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            _playerInput = context.ReadValue<Vector2>();
            if (context.performed) _canMove = true;
            else if (context.canceled)
            {
                _rigidbody2D.velocity = Vector2.zero;
                _canMove = false;
            }
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


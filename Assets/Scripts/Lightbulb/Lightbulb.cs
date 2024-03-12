using GameManager;
using Jim;
using UnityEngine;
using UnityEngine.InputSystem;

public class Lightbulb : MonoBehaviour
{
    public bool lightbulbEnabled;
    [SerializeField] private Sprite lightbulbLit;
    [SerializeField] private Sprite lightbulbUnlit;
    [SerializeField] private AudioClip lightbulbOnSound;
    [SerializeField] private AudioClip lightbulbOffSound;
    private SpriteRenderer _spriteRenderer;
    private bool _canInteractWithLightbulb;
    [SerializeField] private PuzzleManager puzzleManager;
    private JimMovement _jimMovement;

    private void Start()
    {
        _jimMovement = FindObjectOfType<JimMovement>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player")) _canInteractWithLightbulb = true;
        if (other.gameObject.CompareTag("Jim") && _jimMovement.canUseJim)
        {
            Debug.Log("Can Use Jim: " + _jimMovement.canUseJim);
            _canInteractWithLightbulb = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player")) _canInteractWithLightbulb = false;
        if (other.gameObject.CompareTag("Jim") && _jimMovement.canUseJim)
        {
            Debug.Log("Can Use Jim: " + _jimMovement.canUseJim);
            _canInteractWithLightbulb = false;
        }
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        if (_canInteractWithLightbulb) ToggleLightbulb();

    }

    void ToggleLightbulb()
    {
        if (!lightbulbEnabled)
        {
            EnableLightbulb();
        }
        else
        {
            DisableLightbulb();
        }
        puzzleManager.CheckPuzzle();
    }

    void EnableLightbulb()
    {
        _spriteRenderer.sprite = lightbulbLit;
        AudioSource.PlayClipAtPoint(lightbulbOnSound, transform.position);
        lightbulbEnabled = true;
    }

    public void DisableLightbulb()
    {
        _spriteRenderer.sprite = lightbulbUnlit;
        // AudioSource.PlayClipAtPoint(lightbulbOffSound, transform.position);
        lightbulbEnabled = false;
        puzzleManager.ReversePuzzle();
    }
}

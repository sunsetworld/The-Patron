using GameManager;
using Jim;
using UnityEngine;
using UnityEngine.InputSystem;
public class Door : MonoBehaviour
{
    [SerializeField] private GameObject doorFront;
    private bool _canOpenDoor;
    private bool _completedPuzzle;
    [SerializeField] private int level;
    private GameManager.GameManager _gameManager;
    private LevelManager _levelManager;
    private JimMovement _jimMovement;

    private void Start()
    {
        _gameManager = FindObjectOfType<GameManager.GameManager>();
        _levelManager = FindObjectOfType<LevelManager>();
        _jimMovement = FindObjectOfType<JimMovement>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("Player")) return;
        if (!_completedPuzzle) return;
        _canOpenDoor = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("Player")) return;
        if (!_completedPuzzle) return;
        _canOpenDoor = false;

    }

    public void OpenDoor()
    {
        doorFront.gameObject.SetActive(false);
        _completedPuzzle = true;
    }

    public void CloseDoor()
    {
        doorFront.gameObject.SetActive(true);
        _completedPuzzle = false;
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        if (!_canOpenDoor) return;
        if (_jimMovement.canUseJim) return;
        _levelManager.OpenNextLevel();
    }
}

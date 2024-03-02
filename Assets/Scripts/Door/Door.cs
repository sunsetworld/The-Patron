using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    [SerializeField] private GameObject doorFront;
    private bool _canOpenDoor;
    private bool _completedPuzzle;
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

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        if (!_canOpenDoor) return;
        OpenNextLevel();
    }

    void OpenNextLevel()
    {
        int nextLevel = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(nextLevel);
        // Currently just reloads the current level, for now.
    }
}

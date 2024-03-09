using UnityEngine;

public class Key : MonoBehaviour
{
    private GameManager.GameManager _gameManager;

    private void Start()
    {
        _gameManager = FindObjectOfType<GameManager.GameManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("Player")) return;
        _gameManager.playerHasKey = true;
        gameObject.SetActive(false);
    }
}

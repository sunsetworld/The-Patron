using System;
using UnityEngine;

public class LightLock : MonoBehaviour
{
    private GameManager.GameManager _gameManager;

    private void Start()
    {
        _gameManager = FindObjectOfType<GameManager.GameManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Jim"))
        {
            if (!_gameManager.playerHasKey) return;
            _gameManager.playerHasKey = false;
            gameObject.SetActive(false);
        }
    }
}

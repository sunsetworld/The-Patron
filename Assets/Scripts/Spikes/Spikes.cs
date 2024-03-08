using System.Collections;
using GameManager;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    private LevelManager _levelManager;
    [SerializeField] private float respawnTime = 3;

    private void Start()
    {
        _levelManager = FindObjectOfType<LevelManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player")) KillPlayer(other.gameObject);
    }

    void KillPlayer(GameObject player)
    {
        Destroy(player);

        StartCoroutine(RespawnPlayer());
    }

    IEnumerator RespawnPlayer()
    {
        yield return new WaitForSeconds(respawnTime);
        RemoveBread();
        _levelManager.PlayGame();
    }

    void RemoveBread()
    {
        GameObject[] bread = GameObject.FindGameObjectsWithTag("Bread");
        foreach (var slices in bread) Destroy(slices);
    }
}

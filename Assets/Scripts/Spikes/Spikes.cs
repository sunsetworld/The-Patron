using System.Collections;
using GameManager;
using Jim;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    private LevelManager _levelManager;
    [SerializeField] private float respawnTime = 3;
    private JimMovement _jimMovement;

    private void Start()
    {
        _levelManager = FindObjectOfType<LevelManager>();
        _jimMovement = FindObjectOfType<JimMovement>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player")) KillPlayer(other.gameObject);
    }

    void KillPlayer(GameObject player)
    {
        Destroy(player);
        _jimMovement.canUseJim = false;
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

using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KillObject : MonoBehaviour
{
    private int _currentScene;

    private void Start()
    {
        _currentScene = SceneManager.GetActiveScene().buildIndex;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player")) StartCoroutine(KillPlayer(other.gameObject));
    }

    IEnumerator KillPlayer(GameObject player)
    {
        Destroy(player);
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(_currentScene);
    }
}

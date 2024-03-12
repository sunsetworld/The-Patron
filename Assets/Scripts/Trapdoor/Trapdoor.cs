using Player;
using UnityEngine;

public class Trapdoor : MonoBehaviour
{
    private bool _playerHasCollided;
    
    [SerializeField, Range(-1f, -20f)] private float damageThreshold = -12f;
    [SerializeField] private AudioClip breakSound;
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!other.gameObject.CompareTag("Player")) return;
        if (_playerHasCollided) return;
        _playerHasCollided = true;
        PlayerMovement playerMovement = other.gameObject.GetComponent<PlayerMovement>();
        if (playerMovement.downwardVelocity <= damageThreshold)
        {
            AudioSource.PlayClipAtPoint(breakSound, transform.position);
            gameObject.SetActive(false);
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (!other.gameObject.CompareTag("Player")) return;
        _playerHasCollided = false;
    }
}

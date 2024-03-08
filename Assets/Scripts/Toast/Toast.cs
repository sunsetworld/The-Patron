using System;
using UnityEngine;

public class Toast : MonoBehaviour
{
    private Rigidbody2D _toastRb;
    [SerializeField] private float toastMovementSpeed = 5;

    private void Start()
    {
        _toastRb = GetComponent<Rigidbody2D>();
        _toastRb.velocity = transform.up * toastMovementSpeed;
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        DestroyBread(other.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        DestroyBread(other.gameObject);
    }

    private void DestroyBread(GameObject other)
    {
        if (other.CompareTag("Player")) return;
        if (other.CompareTag("Toaster")) return;
        Destroy(this.gameObject);
    }
}

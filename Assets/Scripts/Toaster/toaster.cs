using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class toaster : MonoBehaviour
{
    [SerializeField] private GameObject bread;
    [SerializeField] private float cookTime = 3;
    [SerializeField] private Transform breadPoint;
    [SerializeField] private Sprite toastUp;
    [SerializeField] private Sprite toastDown;
    private SpriteRenderer _spriteRenderer;
    private bool _canShootBread = true;
    private bool _playerIsUsingToaster;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        if (!_playerIsUsingToaster) return;
        if (!_canShootBread) return;
        StartCoroutine(ShootOutBread());
    }

    IEnumerator ShootOutBread()
    {
        _canShootBread = false;
        _spriteRenderer.sprite = toastDown;
        yield return new WaitForSeconds(cookTime);
        if (bread == null)
        {
            Debug.LogWarning("There is no bread >:(");
            Debug.Break();
        }
        else
        {
            _spriteRenderer.sprite = toastUp;
            Instantiate(bread, breadPoint.position, transform.rotation);
            StartCoroutine(ResetToaster());
        }
    }

    IEnumerator ResetToaster()
    {
        yield return new WaitForSeconds(cookTime);
        _canShootBread = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player")) _playerIsUsingToaster = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player")) _playerIsUsingToaster = false;
    }
}

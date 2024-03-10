using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Jim
{
    public class JimMachine : MonoBehaviour
    {
        private JimMovement _jimMovement;
        [SerializeField] private Sprite jimMachineActivateSprite;
        [SerializeField] SpriteRenderer spriteRenderer;
        // Start is called before the first frame update
        void Start()
        {
            _jimMovement = FindObjectOfType<JimMovement>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.gameObject.CompareTag("Player")) return;
            if (!_jimMovement.activateJim) ActivateJimMachine();
        }

        void ActivateJimMachine()
        {
            _jimMovement.activateJim = true;
            spriteRenderer.sprite = jimMachineActivateSprite;

        }
    }
}


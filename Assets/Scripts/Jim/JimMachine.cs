using UnityEngine;
using GameHUD;

namespace Jim
{
    public class JimMachine : MonoBehaviour
    {
        private JimMovement _jimMovement;
        [SerializeField] private Sprite jimMachineActivateSprite;
        [SerializeField] private Sprite jimMachineDeactivateSprite;
        [SerializeField] SpriteRenderer spriteRenderer;
        private CharacterSwap _characterSwap;
        
        // Start is called before the first frame update
        void Start()
        {
            _jimMovement = FindObjectOfType<JimMovement>();
            _characterSwap = FindObjectOfType<CharacterSwap>();
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
            _characterSwap.EnableCanvas();
        }

        public void DeactivateJimMachine()
        {
            _jimMovement.activateJim = false;
            spriteRenderer.sprite = jimMachineDeactivateSprite;
        }
    }
}


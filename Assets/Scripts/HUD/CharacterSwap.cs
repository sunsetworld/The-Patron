using UnityEngine;
using DG.Tweening;

namespace GameHUD
{
    public class CharacterSwap : MonoBehaviour
    {
        [SerializeField] private float characterSwapCanvasDuration = 3;
        public bool firstSwap;

        private CanvasGroup _canvasGroup;
        // Start is called before the first frame update
        void Start()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
        }

        public void EnableCanvas()
        {
            _canvasGroup.DOFade(1, characterSwapCanvasDuration).SetDelay(characterSwapCanvasDuration);
        }

        public void DisableCanvas()
        {
            _canvasGroup.DOFade(0, characterSwapCanvasDuration).SetDelay(characterSwapCanvasDuration);
        }
    }

}

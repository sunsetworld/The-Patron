using Jim;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class HUD : MonoBehaviour
{
    private Image _characterHUDImage;
    [SerializeField] private Sprite wilboImage;
    [SerializeField] private Sprite jimImage;
    private JimMovement _jimMovement;
    private CanvasGroup _canvasGroup;
    [SerializeField] private float canvasDurationTime = 3;

    private void Start()
    {
        _jimMovement = FindObjectOfType<JimMovement>();
        _characterHUDImage = GetComponentInChildren<Image>();
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    public void SwitchCharacter()
    {
        if (_jimMovement.canUseJim) _characterHUDImage.sprite = jimImage;
        else _characterHUDImage.sprite = wilboImage;
    }

    public void GameStart()
    {
        _canvasGroup.DOFade(1, canvasDurationTime);
    }

    public void GameEnd()
    {
        _canvasGroup.DOFade(0, canvasDurationTime);
    }
}

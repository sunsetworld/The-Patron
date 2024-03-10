using Jim;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    private Image _characterHUDImage;
    [SerializeField] private Sprite wilboImage;
    [SerializeField] private Sprite jimImage;
    private JimMovement _jimMovement;

    private void Start()
    {
        _jimMovement = FindObjectOfType<JimMovement>();
        _characterHUDImage = GetComponentInChildren<Image>();
    }

    public void SwitchCharacter()
    {
        if (_jimMovement.canUseJim) _characterHUDImage.sprite = jimImage;
        else _characterHUDImage.sprite = wilboImage;


    }
}

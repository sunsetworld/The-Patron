using System;
using UnityEngine;
using DG.Tweening;
using GameManager;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    private CanvasGroup _canvasGroup;

    [SerializeField] private float openFadeDuration = 3;

    private GameManager.GameManager _gameManager;

    private LevelManager _levelManager;

    [SerializeField] private Button resumeButton;
    // Start is called before the first frame update
    void Start()
    {
        _canvasGroup = GetComponentInChildren<CanvasGroup>();
        _gameManager = FindObjectOfType<GameManager.GameManager>();
        _levelManager = FindObjectOfType<LevelManager>();
        _canvasGroup.DOFade(1, openFadeDuration);
        SetResumeButton();
    }

    private void OnEnable()
    {
        SetResumeButton();
    }

    public void CanvasGroupZero()
    {
        _canvasGroup.alpha = 0;
        _canvasGroup.DOFade(1, openFadeDuration);
    }

    void SetResumeButton()
    {
        int currentLevel = PlayerPrefs.GetInt("PlayerLevel");
        Debug.Log("Current Level (Main Menu)" + currentLevel);
        if (currentLevel > 0) resumeButton.interactable = true;
        else if (currentLevel == 0) resumeButton.interactable = false;
    }

    public void OnNewGame()
    {
        _levelManager.StartNewGame();
        _levelManager.PlayGame();
    }

    public void OnResumeGame()
    {
        _levelManager.PlayGame();
    }

    public void OnQuit()
    {
        Application.Quit();
    }
    
}

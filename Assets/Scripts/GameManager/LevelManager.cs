using Cinemachine;
using UnityEngine;
using DG.Tweening;
using Unity.Mathematics;
using UnityEngine.Tilemaps;

namespace GameManager
{
    public class LevelManager : MonoBehaviour
    {
        public int currentLevel;
        private GameManager _gameManager;
        [SerializeField] private float playerXMoveAmount = 26;
        [SerializeField] private GameObject[] cameras;
        [SerializeField] private Transform[] spawnLocations;
        [SerializeField] GameObject playerObj;
        private GameObject _spawnedPlayer;
        [SerializeField] private float playerMoveDuration = 3;
        [SerializeField] private bool useTweenForPlayerSpawnMove;
        [SerializeField] private bool canResetCurrentPlayerLevel;
        private CinemachineBrain _cinemachineBrain;
        [SerializeField] private TilemapCollider2D wallCollider2d;
        

        // Start is called before the first frame update
        void Start()
        {
            _gameManager = FindObjectOfType<GameManager>();
            _cinemachineBrain = FindObjectOfType<CinemachineBrain>();
            if (canResetCurrentPlayerLevel) StartNewGame();
            if (!_gameManager.debugMode) ResumeCurrentGame();
            ChooseNewCamera();
            PlayGame();
        }

        public void PlayGame()
        {
            _spawnedPlayer = 
                Instantiate(playerObj, spawnLocations[currentLevel].position, quaternion.identity);
        }

        void StartNewGame()
        {
            PlayerPrefs.SetInt("PlayerLevel", 0);
        }

        void ResumeCurrentGame()
        {
            currentLevel = PlayerPrefs.GetInt("PlayerLevel");
        }

        public void OpenNextLevel()
        {
            currentLevel++;
            Debug.Log("Current Level" + currentLevel);
            if (!_gameManager.debugMode) PlayerPrefs.SetInt("PlayerLevel", currentLevel);
            ChooseNewCamera();
            MovePlayerToNewSpawnLocation();
        }

        void MovePlayerToNewSpawnLocation()
        {
            if (useTweenForPlayerSpawnMove)
            {
                wallCollider2d.enabled = false;
                _spawnedPlayer.transform.DOMove(spawnLocations[currentLevel].position, playerMoveDuration)
                    .OnComplete(() => wallCollider2d.enabled = true);
                
            }
            else _spawnedPlayer.transform.position = spawnLocations[currentLevel].position;

        }

        void ChooseNewCamera()
        {
            for (int i = 0; i < cameras.Length; i++)
            {
                if (currentLevel == i) cameras[i].SetActive(true);
                else cameras[i].SetActive(false);
            }
        }
    }
}


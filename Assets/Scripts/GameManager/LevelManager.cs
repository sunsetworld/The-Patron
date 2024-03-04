using UnityEngine;
using DG.Tweening;
using Unity.Mathematics;

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

        // Start is called before the first frame update
        void Start()
        {
            _gameManager = FindObjectOfType<GameManager>();
            if (!_gameManager.debugMode) currentLevel = PlayerPrefs.GetInt("PlayerLevel");
            _spawnedPlayer = 
                Instantiate(playerObj, spawnLocations[currentLevel].position, quaternion.identity);
            if (_spawnedPlayer == null) Debug.Break();

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
                _spawnedPlayer.transform.DOMove(spawnLocations[currentLevel].position, playerMoveDuration);
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


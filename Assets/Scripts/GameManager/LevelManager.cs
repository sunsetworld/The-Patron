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
        [SerializeField] private GameObject MainMenuCanvas;
        [SerializeField] private GameObject hudCanvas;
        private MainMenu _mainMenu;
        [SerializeField] private GameObject trapdoorObj;
        private GameObject[] _trapdoors;
        

        // Start is called before the first frame update
        void Start()
        {
            _gameManager = FindObjectOfType<GameManager>();
            _cinemachineBrain = FindObjectOfType<CinemachineBrain>();
            _mainMenu = FindObjectOfType<MainMenu>();
            _trapdoors = GameObject.FindGameObjectsWithTag("Trapdoor");
            currentLevel = PlayerPrefs.GetInt("PlayerLevel");
            // if (currentLevel > 0) ChooseNewCamera();
        }

        public void PlayGame()
        {
            MainMenuCanvas.SetActive(false);
            hudCanvas.SetActive(true);
            _gameManager.PlayMusic();
            ResetGameComponents();
            ChooseNewCamera();
            GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
            if (players.Length > 0) return;
            _spawnedPlayer = 
                Instantiate(playerObj, spawnLocations[currentLevel].position, quaternion.identity);
        }

        void ResetGameComponents()
        {
            Lightbulb[] lightbulbs = FindObjectsOfType<Lightbulb>();
            foreach (var lightbulb in lightbulbs) lightbulb.DisableLightbulb();
            Door[] doors = FindObjectsOfType<Door>();
            foreach (var levelDoor in doors) levelDoor.CloseDoor();
            _trapdoors = GameObject.FindGameObjectsWithTag("Trapdoor");
            foreach (var trapdoor in _trapdoors) trapdoor.SetActive(true);
            GameObject[] keys = GameObject.FindGameObjectsWithTag("Key");
            foreach (var gameKey in keys) gameKey.SetActive(true);
            GameObject[] lightLocks = GameObject.FindGameObjectsWithTag("Lightlock");
            foreach (var lightLock in lightLocks) lightLock.SetActive(true);
        }

        public void StartNewGame()
        {
            PlayerPrefs.SetInt("PlayerLevel", 0);
            currentLevel = PlayerPrefs.GetInt("PlayerLevel");
        }

        public void OpenNextLevel()
        {
            if (currentLevel == spawnLocations.Length - 1)
            {
                EndGame();
                return;
            }
            currentLevel++;
            Debug.Log("Current Level" + currentLevel);
            if (!_gameManager.debugMode) PlayerPrefs.SetInt("PlayerLevel", currentLevel);
            ChooseNewCamera();
            MovePlayerToNewSpawnLocation();
        }

        void EndGame()
        {
            Destroy(_spawnedPlayer);
            Debug.Log("Game Over.");
            currentLevel = 0;
            StartNewGame();
            if (_mainMenu != null) _mainMenu.CanvasGroupZero();
            MainMenuCanvas.SetActive(true);
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


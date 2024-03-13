using Cinemachine;
using UnityEngine;
using DG.Tweening;
using Jim;
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
        private HUD _hud;
        private MainMenu _mainMenu;
        [SerializeField] private GameObject trapdoorObj;
        private GameObject[] _trapdoors;
        private JimMachine _jimMachine;
        public GameObject[] destroyableObjects;
        private MusicManager _musicManager;

        [Header("Jim")] 
        [SerializeField] Transform[] jimSpawnLocations;
        private int _jimLevel;
        [SerializeField] private int jimLevelThreshhold = 6;
        private JimMovement _jimMovement;
        [SerializeField] private GameObject jimObj;
        

        // Start is called before the first frame update
        void Start()
        {
            _jimMachine = GameObject.FindObjectOfType<JimMachine>();
            _jimMovement = FindObjectOfType<JimMovement>();
            _gameManager = FindObjectOfType<GameManager>();
            _cinemachineBrain = FindObjectOfType<CinemachineBrain>();
            _mainMenu = FindObjectOfType<MainMenu>();
            _hud = FindObjectOfType<HUD>();
            _musicManager = GetComponent<MusicManager>();
            _trapdoors = GameObject.FindGameObjectsWithTag("Trapdoor");
            currentLevel = PlayerPrefs.GetInt("PlayerLevel");
            jimObj = GameObject.FindGameObjectWithTag("Jim");
        }

        public void PlayGame()
        {
            MainMenuCanvas.SetActive(false);
            hudCanvas.SetActive(true);
            _hud.GameStart();
            _musicManager.PlaySoundtrack();
            ResetGameComponents();
            ChooseNewCamera();
            GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
            if (players.Length > 0) return;
            _spawnedPlayer = 
                Instantiate(playerObj, spawnLocations[currentLevel].position, quaternion.identity);
            if (currentLevel >= jimLevelThreshhold)
            {
                _jimMovement.activateJim = true;
                MoveJim();
            }
        }

        void ResetGameComponents()
        {
            Lightbulb[] lightbulbs = FindObjectsOfType<Lightbulb>();
            foreach (var lightbulb in lightbulbs) lightbulb.DisableLightbulb();
            Door[] doors = FindObjectsOfType<Door>();
            foreach (var levelDoor in doors) levelDoor.CloseDoor();
            _jimMachine.DeactivateJimMachine();
            foreach (var obj in destroyableObjects) obj.SetActive(true);
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
            _hud.GameEnd();
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
                MoveJim();
            }
            else
            {
                _spawnedPlayer.transform.position = spawnLocations[currentLevel].position;
            }
        }

        void MoveJim()
        {
            if (_jimMovement.activateJim)
            {
                _jimLevel++;
                wallCollider2d.enabled = false;
                jimObj.transform.DOMove(jimSpawnLocations[_jimLevel].position, playerMoveDuration)
                    .OnComplete(() => wallCollider2d.enabled = true);
            }
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


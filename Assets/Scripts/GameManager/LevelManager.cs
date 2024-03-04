using UnityEngine;

namespace GameManager
{
    public class LevelManager : MonoBehaviour
    {
        public int currentLevel;
        private GameManager _gameManager;
        private Transform _playerTransform;
        [SerializeField] private float playerXMoveAmount = 26;
        [SerializeField] private GameObject[] cameras;

        // Start is called before the first frame update
        void Start()
        {
            _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
            _gameManager = FindObjectOfType<GameManager>();
            if (_gameManager.debugMode) currentLevel = PlayerPrefs.GetInt("PlayerLevel"); 
            // Saves level ONLY when the game is not in Debug Mode.
        }

        public void OpenNextLevel()
        {
            currentLevel++;
            if (!_gameManager.debugMode) PlayerPrefs.SetInt("PlayerLevel", currentLevel);
            ChooseNewCamera();
            _playerTransform.position = new Vector3(_playerTransform.position.x + playerXMoveAmount, 3.26f, 0);
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


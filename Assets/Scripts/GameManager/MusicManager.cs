using UnityEngine;

namespace GameManager
{
    public class MusicManager : MonoBehaviour
    {
        private AudioSource _audioSource;
        // Start is called before the first frame update
        void Start()
        {
            _audioSource = GetComponent<AudioSource>();
        }
        
        public void PlaySoundtrack()
        {
            if (!_audioSource.isPlaying) _audioSource.Play();
        }
    }

}


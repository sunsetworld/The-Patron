using UnityEngine;

namespace GameManager
{
    public class PuzzleManager : MonoBehaviour
    {
        [SerializeField] private Door doorObj;

        [SerializeField] private bool[] lightbulbPattern;
        [SerializeField] private Lightbulb[] lightbulbs;
        private bool _hasPlayerCompletedPuzzle;
        [SerializeField] private AudioClip puzzleCompleteSound;

        public void CheckPuzzle()
        {
            for (int i = 0; i < lightbulbPattern.Length; i++)
            {
                if (lightbulbs[i].lightbulbEnabled != lightbulbPattern[i])
                {
                    _hasPlayerCompletedPuzzle = false;
                    break;
                }
                if (lightbulbs[i].lightbulbEnabled == lightbulbPattern[i])
                {
                    _hasPlayerCompletedPuzzle = true;
                }
            }
            if (_hasPlayerCompletedPuzzle) CompletedPuzzle();
        }

        void CompletedPuzzle()
        {
            AudioSource.PlayClipAtPoint(puzzleCompleteSound, transform.position);
            doorObj.OpenDoor();
        }
    }

}

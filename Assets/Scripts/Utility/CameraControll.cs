using UnityEngine;

namespace Utility
{
    public class CameraControll : MonoBehaviour
    {
        // Variables
        private GameManager _gameManager;
        private AudioSource _cameraAudio; // AudioSource component
        
        public AudioClip music; // Music played while playing the game
    
        // Actual Code
        void Start()
        {
            _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
            _cameraAudio = GetComponent<AudioSource>(); // Get AudioSource component from camera
        }
    }
}

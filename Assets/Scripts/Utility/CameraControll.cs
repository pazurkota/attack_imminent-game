using Player;
using UnityEngine;

namespace Utility
{
    public class CameraControll : MonoBehaviour
    {
        // Variables
        private PlayerController _playerController; // PlayerController.cs component
        private AudioSource _cameraAudio; // AudioSource component
        
        public AudioClip music; // Music played while playing the game
    
        // Actual Code
        void Start()
        {
            _playerController = GameObject.Find("Player").GetComponent<PlayerController>(); // Get PlayerController.cs script
            _cameraAudio = GetComponent<AudioSource>(); // Get AudioSource component from camera
            
            _cameraAudio.PlayOneShot(music, 1.0f);
        }

        // Update is called once per frame
        void Update()
        {
            if (_playerController.gameOver) // If game over is true, stop playing the music
            {
                _cameraAudio.Stop();
            }
        }
    }
}

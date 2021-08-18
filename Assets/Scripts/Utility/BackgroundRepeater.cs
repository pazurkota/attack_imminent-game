using Player;
using UnityEngine;

namespace Utility
{
    public class BackgroundRepeater : MonoBehaviour
    {
        public Renderer renderTerrain;
        private float _tempScroll;
        private PlayerController _playerController;

        private void Start()
        {
            _playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        }

        // Update is called once per frame
        void Update()
        {
            if (_playerController.gameOver == false)
            {
                _tempScroll += 0.0001f;
                renderTerrain.material.mainTextureOffset = new Vector2(0, -_tempScroll);
            }
        }
    }
}

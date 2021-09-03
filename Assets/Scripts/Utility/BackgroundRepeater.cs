using Player;
using UnityEngine;

namespace Utility
{
    public class BackgroundRepeater : MonoBehaviour
    {
        public Renderer renderTerrain;
        private float _tempScroll;
        private GameManager _gameManager;

        private void Start()
        {
            _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        }

        // Update is called once per frame
        void Update()
        {
            if (_gameManager.gameRunning)
            {
                _tempScroll += 0.1f * Time.deltaTime;
                renderTerrain.material.mainTextureOffset = new Vector2(0, -_tempScroll);
            }
        }
    }
}

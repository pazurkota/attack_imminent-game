using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControll : MonoBehaviour
{
    private PlayerController _playerController;
    private AudioSource cameraAudio;
    public AudioClip Music;
    
    // Start is called before the first frame update
    void Start()
    {
        _playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        cameraAudio = GetComponent<AudioSource>();
        
        cameraAudio.PlayOneShot(Music, 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (_playerController.gameOver == true)
        {
            cameraAudio.Stop();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelReset : MonoBehaviour {

    PlayerData playerData;
    CameraController cameraController;

    private void Start()
    {
        playerData = FindObjectOfType<PlayerData>();
        cameraController = FindObjectOfType<CameraController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            playerData.DamagePlayer();

            if (playerData.playerHealth > 0)
            {
                SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
            }
            else
            {
                cameraController.freezeCamera = true;
            }
        }
    }
}

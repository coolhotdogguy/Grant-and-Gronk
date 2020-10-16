using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerData : MonoBehaviour
{
    public int temporalCoagulateInventory;
    PlayerController player;
    bool gronkLevel;

    void Awake()
    {
        if (FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        PlayerController playerController = (PlayerController)FindObjectOfType(typeof(PlayerController));
        if(playerController)
        {
            player = playerController;
        }
        else
        {
            Debug.Log("No player found");
        }
    }



    public void AddToInventoy(GameObject temporalCoagulate)
    {
        int instanceID = temporalCoagulate.gameObject.GetInstanceID();

        temporalCoagulateInventory++;
        /*
        PlayerPrefs.SetInt(instanceID.ToString(), 1); //returns 1 when called
        PlayerPrefs.SetInt("Inventory", temporalCoagulateInventory);
        */

        //player.AddTemporalCoagulateData(temporalCoagulateInventory);
    }

    void OnRInput(InputValue value)
    {
        if (!gronkLevel)
        {
            SceneManager.LoadScene("Gronk Level");
        }
        if (gronkLevel)
        {
            SceneManager.LoadScene("Grant Level");
        }

        gronkLevel = !gronkLevel;
    }

    void OnQInput(InputValue value)
    {

        PlayerController playerController = (PlayerController)FindObjectOfType(typeof(PlayerController));
        if (playerController)
        {
            player = playerController;
            player.UpdateInventory();
        }
        else
        {
            Debug.Log("No player found");
        }
    }
}

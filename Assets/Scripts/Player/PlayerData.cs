﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using UnityEngine.WSA;

public class PlayerData : MonoBehaviour
{
    public int temporalCoagulateInventory;
    public bool gronkLevel;
    int playerHealth = 3;
    [SerializeField] Image[] healthUnits;
    [SerializeField] GameObject temporalCoagulatesFolder;
    public Vector2 playerPosition;
    public int currentPlanetType;

    void Start()
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


    private void Update()
    {
        HandleHealthUI();
        HandleTemporalCoagulateVisibility();
    }

    private void HandleTemporalCoagulateVisibility()
    {
        if (!gronkLevel)
        {
            temporalCoagulatesFolder.SetActive(true);
        }
        else
        {
            temporalCoagulatesFolder.SetActive(false);
        }
    }

    private void HandleHealthUI()
    {
        if (!gronkLevel)
        {

            if (playerHealth == 3)
            {
                healthUnits[0].enabled = true;
                healthUnits[1].enabled = true;
                healthUnits[2].enabled = true;
            }
            if (playerHealth == 2)
            {
                healthUnits[0].enabled = true;
                healthUnits[1].enabled = true;
                healthUnits[2].enabled = false;
            }
            if (playerHealth == 1)
            {
                healthUnits[0].enabled = true;
                healthUnits[1].enabled = false;
                healthUnits[2].enabled = false;
            }
            if (playerHealth <= 0)
            {
                healthUnits[0].enabled = false;
                healthUnits[1].enabled = false;
                healthUnits[2].enabled = false;
            }
        }
        else
        {
            healthUnits[0].enabled = false;
            healthUnits[1].enabled = false;
            healthUnits[2].enabled = false;
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
    }

    public void DamagePlayer()
    {
        playerHealth--;
    }

    public void GetGrantPosition(Vector2 position)
    {
        playerPosition = position;
    }

    public void SetPlanetType(int i)
    {
        currentPlanetType = i;
    }

    public int SendCurrentPlanetType()
    {
        return currentPlanetType;
    }
        

    void OnRInput(InputValue value)
    {
        if (!gronkLevel) //Load Gronk Level
        {
            playerPosition = FindObjectOfType<PlayerController>().transform.position;
            SceneManager.LoadScene("Gronk Level");

        }
        if (gronkLevel)  //Load Grant Level
        {
            SceneManager.LoadScene("Grant Level");
        }

        gronkLevel = !gronkLevel;
    }

}

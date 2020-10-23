using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using TMPro;

public class PlayerData : MonoBehaviour
{



    //CHEAT, delete
   /* void OnXInput(InputValue value)
    {
        collectedTemporalCoagulateInt += 5;
        inventoryText.text = collectedTemporalCoagulateInt.ToString();
    }*/



    public int collectedTemporalCoagulateInt;
    public bool gronkLevel;
    public int playerHealth = 4;
    [SerializeField] Image[] healthUnits;
    [SerializeField] TextMeshProUGUI inventoryText;
    [SerializeField] GameObject temporalCoagulatesFolder;
    [HideInInspector] public Vector2 playerPosition;
    [SerializeField] GameObject groundGameObject;
    GameObject levelObjectsGameObject;
    [HideInInspector] public bool icePlanet; //these two are used to track planet type to allow TC refund in Gronk Level
    [HideInInspector] public bool dryPlanet;
    [SerializeField] TilemapSwapper2 tileSwapper;
    [HideInInspector] public bool enableBugs = true;

    void Start()
    {
        if (FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        };

        levelObjectsGameObject = FindObjectOfType<LevelObjects>().gameObject;

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

    public void HandleHealthUI()
    {
        if (!gronkLevel && !FindObjectOfType<PlayerController>().invincible)
        {

            if (playerHealth == 4)
            {
                healthUnits[0].enabled = true;
                healthUnits[1].enabled = true;
                healthUnits[2].enabled = true;
            }
            if (playerHealth == 3)
            {
                healthUnits[0].enabled = true;
                healthUnits[1].enabled = true;
                healthUnits[2].enabled = false;
            }
            if (playerHealth == 2)
            {
                healthUnits[0].enabled = true;
                healthUnits[1].enabled = false;
                healthUnits[2].enabled = false;
            }
            if (playerHealth <= 1)
            {
                healthUnits[0].enabled = false;
                healthUnits[1].enabled = false;
                healthUnits[2].enabled = false;
            }
        }
        else if (gronkLevel || FindObjectOfType<PlayerController>().invincible)
        {
            healthUnits[0].enabled = false;
            healthUnits[1].enabled = false;
            healthUnits[2].enabled = false;
        }
    }

    public void AddToInventoy(int i)
    {
        collectedTemporalCoagulateInt += i;
        inventoryText.text = collectedTemporalCoagulateInt < 10 ? "0" + collectedTemporalCoagulateInt.ToString() : collectedTemporalCoagulateInt.ToString();
    }

    public void SubtractFromInventroy(int reqTempCoag, int planetType)
    {
        collectedTemporalCoagulateInt -= reqTempCoag;
        inventoryText.text = collectedTemporalCoagulateInt.ToString();
        if (planetType == 1)
        {
            icePlanet = true;
        }
        if (planetType == 2)
        {
            dryPlanet = true;
        }
    }

    public void DamagePlayer()
    {
        playerHealth--;
        HandleHealthUI();
        if (playerHealth <= 0)
        {
            StartCoroutine(HandleDyingCoroutine());
        }
    }

    IEnumerator HandleDyingCoroutine()
    {
        FindObjectOfType<CameraController>().freezeCamera = true;
        yield return new WaitForSeconds(2f);
        OnDeathReset();
    }

    void OnDeathReset()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        FindObjectOfType<CameraController>().freezeCamera = false;

        FindObjectOfType<PlayerData>().icePlanetUnlocked = false;
        FindObjectOfType<PlayerData>().dryPlanetUnlocked = false;
        FindObjectOfType<TempCoagManager>().EnableAllTemporalCoagulate();
        collectedTemporalCoagulateInt = 0;
        inventoryText.text = collectedTemporalCoagulateInt.ToString();

        playerHealth = 4;
        HandleHealthUI();

        playerPosition = Vector2.zero;
    }


    public void GetGrantPosition(Vector2 position)
    {
        playerPosition = position;
    }

    void OnRInput(InputValue value)
    {
        if (!gronkLevel) //Load Gronk Level
        {
            LoadGronkLevel();

        }
        if (gronkLevel)  //Load Grant Level
        {
            LoadGrantLevel();
        }

        gronkLevel = !gronkLevel;

        HandleHealthUI();
        HandleTemporalCoagulateVisibility();
        FindObjectOfType<SFXPlayer>().PlayTimeTravelSound();
    }


    [HideInInspector] public int planetSwitcherCounter;
    [HideInInspector] public int previousPlanet;
    [HideInInspector] public bool icePlanetUnlocked;
    [HideInInspector] public bool dryPlanetUnlocked;
    void OnEInput(InputValue value)
    {
        if (gronkLevel) { return; }

        if (value.isPressed)
        {

            planetSwitcherCounter++;
            if (!icePlanetUnlocked) { planetSwitcherCounter = 0; }
            if (!dryPlanetUnlocked && planetSwitcherCounter == 2) { planetSwitcherCounter = 0; }
            if (planetSwitcherCounter > 2)
            {
                planetSwitcherCounter = 0;
            }

            FindObjectOfType<TilemapSwapper2>().currentPlanetType = planetSwitcherCounter;

            if (planetSwitcherCounter == 0)
            {
                SwitchToForestPlanet();
            }
            if (planetSwitcherCounter == 1 && icePlanetUnlocked == true)
            {
                SwitchToIcePlanet();
            }
            if (planetSwitcherCounter == 2 && dryPlanetUnlocked == true)
            {
                SwitchToDryPlanet();
            }


            FindObjectOfType<BackgroundManager>().EnableBGs(planetSwitcherCounter);
            if (previousPlanet != planetSwitcherCounter)
            {
                FindObjectOfType<Icons>().FadeInPlanetIcon(planetSwitcherCounter);
            }

            previousPlanet = planetSwitcherCounter;
        }

    } //cycle planetTypes


    public void SwitchToDryPlanet()
    {
        FindObjectOfType<Icons>().FadeOutPlanetIcon(1);
        tileSwapper.SetPlanetTypeInt(2);
        tileSwapper.UpdateTilesAndObjects();
        FindObjectOfType<RabbitManager>().HandleRabbitVisibility(2);
        FindObjectOfType<PlayerController>().icePlanet = false;
        FindObjectOfType<SFXPlayer>().SwitchAmbience(2);
    }

    public void SwitchToIcePlanet()
    {
        FindObjectOfType<Icons>().FadeOutPlanetIcon(0);
        tileSwapper.SetPlanetTypeInt(1);
        tileSwapper.UpdateTilesAndObjects();
        FindObjectOfType<RabbitManager>().HandleRabbitVisibility(1);
        FindObjectOfType<PlayerController>().icePlanet = true;
        FindObjectOfType<SFXPlayer>().SwitchAmbience(1);
    }

    public void SwitchToForestPlanet()
    {
        if (icePlanetUnlocked && !dryPlanetUnlocked)
        {
            FindObjectOfType<Icons>().FadeOutPlanetIcon(1);
        }
        if (dryPlanetUnlocked)
        {
            FindObjectOfType<Icons>().FadeOutPlanetIcon(2);
        }
        tileSwapper.SetPlanetTypeInt(0);
        tileSwapper.UpdateTilesAndObjects();
        FindObjectOfType<RabbitManager>().HandleRabbitVisibility(0);
        FindObjectOfType<PlayerController>().icePlanet = false;
        FindObjectOfType<SFXPlayer>().SwitchAmbience(0);
    }

    private void LoadGrantLevel()
    {
        SceneManager.LoadScene("Grant Level");
        groundGameObject.GetComponent<TilemapRenderer>().enabled = true;
        groundGameObject.GetComponent<TilemapCollider2D>().enabled = true;
        levelObjectsGameObject.SetActive(true);
        tileSwapper.UpdateTilesAndObjects();
        FindObjectOfType<SFXPlayer>().SwitchAmbience(FindObjectOfType<TilemapSwapper2>().currentPlanetType);
    }

    private void LoadGronkLevel()
    {
        playerPosition = FindObjectOfType<PlayerController>().transform.position; //store grant position
        groundGameObject.GetComponent<TilemapRenderer>().enabled = false;
        groundGameObject.GetComponent<TilemapCollider2D>().enabled = false;
        levelObjectsGameObject.SetActive(false);

        SceneManager.LoadScene("Gronk Level");
    }
}

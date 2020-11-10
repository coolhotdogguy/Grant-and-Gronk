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

    private void Update()
    {
        //Debug.Log(planetSwitcherCounter);
    }

    public int collectedTemporalCoagulateInt;
    public bool gronkLevel;
    public int playerHealth = 4;
    [SerializeField] Image[] healthUnits;
    [SerializeField] TextMeshProUGUI inventoryText;
    [SerializeField] GameObject temporalCoagulatesFolder;
    [HideInInspector] public Vector2 playerPosition;
    [SerializeField] GameObject groundGameObject;
    GameObject levelObjectsGameObject;
    [HideInInspector] public PlanetType currentPlanet;
    [SerializeField] TilemapSwapper2 tileSwapper;
    [HideInInspector] public bool enableBugs = true;
    public bool invincible;

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
        if (!gronkLevel && !invincible)
        {
            for (int i = 0; i < playerHealth; i++)
            {
                try
                {
                    healthUnits[i].enabled = false;
                    healthUnits[i - 1].enabled = true;
                }
                catch { }
            }
        }
        else if (gronkLevel || invincible)
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

    // This Func is unused, hmmm
    /*public void SubtractFromInventroy(int reqTempCoag, PlanetType planetType)
    {
        collectedTemporalCoagulateInt -= reqTempCoag;
        inventoryText.text = collectedTemporalCoagulateInt.ToString();
        if (planetType == PlanetType.Ice)
        {
            icePlanet = true;
        }
        if (planetType == 2)
        {
            dryPlanet = true;
        }
    }*/

    public void DamagePlayer()
    {
        if(!invincible) playerHealth--;
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
        inventoryText.text = inventoryText.text = collectedTemporalCoagulateInt < 10 ? "0" + collectedTemporalCoagulateInt.ToString() : collectedTemporalCoagulateInt.ToString();

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

            FindObjectOfType<TilemapSwapper2>().currentPlanetType = (PlanetType)planetSwitcherCounter;

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

            FindObjectOfType<Icons>().FadeInPlanetIcon(planetSwitcherCounter);

            previousPlanet = planetSwitcherCounter;
        }

    } //cycle planetTypes


    public void SwitchToDryPlanet()
    {
        FindObjectOfType<Icons>().FadeOutPlanetIcon(1);
        tileSwapper.SetPlanetTypeInt(PlanetType.Desert);
        tileSwapper.UpdateTilesAndObjects();
        FindObjectOfType<RabbitManager>().HandleRabbitVisibility(PlanetType.Desert);
        FindObjectOfType<PlayerController>().icePlanet = false;
        FindObjectOfType<SFXPlayer>().SwitchAmbience(2);
    }

    public void SwitchToIcePlanet()
    {
        FindObjectOfType<Icons>().FadeOutPlanetIcon(0);
        tileSwapper.SetPlanetTypeInt(PlanetType.Ice);
        tileSwapper.UpdateTilesAndObjects();
        FindObjectOfType<RabbitManager>().HandleRabbitVisibility(PlanetType.Ice);
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
        tileSwapper.SetPlanetTypeInt(PlanetType.Forest);
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
        FindObjectOfType<SFXPlayer>().SwitchAmbience((int)FindObjectOfType<TilemapSwapper2>().currentPlanetType);
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

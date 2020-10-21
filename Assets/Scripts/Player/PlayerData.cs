using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using TMPro;
using UnityEngine.PlayerLoop;

public class PlayerData : MonoBehaviour
{



    //CHEAT, delete
    void OnXInput(InputValue value)
    {
        collectedTemporalCoagulateInt += 5;
        inventoryText.text = collectedTemporalCoagulateInt.ToString();
    }





    public int collectedTemporalCoagulateInt;
    public bool gronkLevel;
    int playerHealth = 3;
    [SerializeField] Image[] healthUnits;
    [SerializeField] TextMeshProUGUI inventoryText;
    [SerializeField] GameObject temporalCoagulatesFolder;
    [HideInInspector] public Vector2 playerPosition;
    [SerializeField] GameObject groundGameObject;
    GameObject levelObjectsGameObject;
    [HideInInspector] public bool icePlanet; //these two are used to track planet type to allow TC refund in Gronk Level
    [HideInInspector] public bool dryPlanet;
    [SerializeField] TilemapSwapper2 tileSwapper;

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

    public void AddToInventoy(int i)
    {
        collectedTemporalCoagulateInt += i;
        inventoryText.text = collectedTemporalCoagulateInt.ToString();
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
        yield return new WaitForSeconds(1f);
        OnDeath();
    }

    void OnDeath()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        FindObjectOfType<TempCoagManager>().EnableAllTemporalCoagulate();
        collectedTemporalCoagulateInt = 0;
        inventoryText.text = collectedTemporalCoagulateInt.ToString();

        playerHealth = 3;
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
    }


    int counter;
    [HideInInspector] public bool icePlanetUnlocked;
    [HideInInspector] public bool dryPlanetUnlocked;
    void OnQInput(InputValue value)
    {

        if (value.isPressed)
        {
            counter++;
            if (!icePlanetUnlocked) { counter = 0; }
            if (!dryPlanetUnlocked && counter == 2) { counter = 0; }
            if (counter > 2)
            {
                counter = 0;
            }
            if (counter == 0)
            {
                SwitchToForestPlanet();
            }
            if (counter == 1 && icePlanetUnlocked == true)
            {
                SwitchToIcePlanet();
            }
            if (counter == 2 && dryPlanetUnlocked == true)
            {
                SwitchToDryPlanet();
            }
        }

    } //cycle planetTypes


    public void SwitchToDryPlanet()
    {
        tileSwapper.SetPlanetTypeInt(2);
        tileSwapper.UpdateTilesAndObjects();
        FindObjectOfType<RabbitManager>().HandleRabbitVisibility(2);
        FindObjectOfType<PlayerController>().icePlanet = false;
    }

    public void SwitchToIcePlanet()
    {
        tileSwapper.SetPlanetTypeInt(1);
        tileSwapper.UpdateTilesAndObjects();
        FindObjectOfType<RabbitManager>().HandleRabbitVisibility(1);
        FindObjectOfType<PlayerController>().icePlanet = true;
    }

    public void SwitchToForestPlanet()
    {
        tileSwapper.SetPlanetTypeInt(0);
        tileSwapper.UpdateTilesAndObjects();
        FindObjectOfType<RabbitManager>().HandleRabbitVisibility(0);
        FindObjectOfType<PlayerController>().icePlanet = false;
    }

    private void LoadGrantLevel()
    {
        SceneManager.LoadScene("Grant Level");
        groundGameObject.GetComponent<TilemapRenderer>().enabled = true;
        groundGameObject.GetComponent<TilemapCollider2D>().enabled = true;
        levelObjectsGameObject.SetActive(true);
        groundGameObject.GetComponent<TilemapSwapper2>().UpdateTilesAndObjects();
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
